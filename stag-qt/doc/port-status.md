# Stan portu STAG: VB6 → Qt

Stan na 2026-11-02. Gałąź `stag-qt-port`. `stag-qt/buildmac/` jest nieśledzony i nie ma go w `.gitignore`.

## W skrócie

Logika jest przepisana poza CAN-em. Nic nie było uruchamiane na prawdziwym PLC, kartach ani SPDE. Testy (33/33) działają na symulatorach i danych z maszyny. Z nowych ekranów obejrzany został tylko główny.

## Co jest przepisane

| Obszar | Pliki w `src/logic/` | Czym sprawdzone |
|---|---|---|
| Ramki i komunikacja z PLC | `plcframes`, `plclink` | `Plc.log`, symulator FINS |
| Karty tnące (UDP) | `schtagframes`, `schtaglink` | 9447 odczytów i 47 zamówień z `Sch_tag*.log` odtworzonych bajt w bajt, symulator |
| `Set_par_sch_tag1–5`, `Leg_cfg`, `Sal_cfg`, `Del_cfg` | `schtagpages`, `schtagtables.inc` (generowane) | Symulator, `stag.ini` i baza z maszyny |
| `Set_par_mac`, silniki w `Abilitazioni`, zapis ini | `schtagpages`, `abilitazionimodel`, `core/inibas` | Zapis ini bez zmian wartości względem `stag.ini` |
| `Set_par_rul`, `Set_par_com` | `setparmodels` | Symulator |
| Odbiór zamówień ze SPDE (Fosber 3.0, UDP) | `spdelink` | 7156 ramek statusu z `Spde.log`, 7 zamówień z `Spde_old.log` |
| `Adj_pat_con`, `Sel_lin`, `Pag_pro_day`, `Cam_tur`, `Con_azz`, `Dia_sch_tag`, `Frm_debug`, `Frm_debug_des`, `Frm_data`, `Imp_all`, `Pag_msg` | `miscpages`, `debugdes.inc` (generowane) | Testy modeli |
| Pasek stanu | `stabarmodel`, `stabarform` | Testy modelu, render ekranu |
| F7 w `Pag_pro` | `pagpromodel`, `gesord` | Testy modelu |

Wcześniejsze strony (listy zamówień, alarmy, fale, wzory, dialogi zamówień, hasła) są w commicie `706cfbe`.

## Czego nie ma

| Co | Dlaczego |
|---|---|
| CAN (`com_lenze.bas`), `Dia_sta_azi`, `Dia_ret_azi`, `Dia_par_new`, `Dia_par_des`, `Dia_par_new_des` | TODO na później. Logi pokazują, że CAN na tej maszynie nie działa. `Set_par_rul` przy zapisie pokazuje „Error” i przywraca stare wartości. Punkt wpięcia: `SetParRulModel::setDriveSender`. |
| Inne protokoły SPDE (Fosber 1.0–2.1, Simon, MHI) | Maszyna ma `ver_spde=4` i Ethernet. Brak danych do sprawdzenia. |
| Komunikacja szeregowa (SPDE, karty) | Maszyna używa sieci. Ustawienia są tylko zapisywane. |
| `Stato_IO` | W VB `Load` jest zakomentowany. Zostaje sam układ. |
| `Frm_ini` | Ekran startowy, w Qt niepotrzebny. |

## Bez pokrycia w logach

Te rzeczy są porównane tylko z kodem VB:

- ramki `CONFIGURA` do kart tnących,
- prawdziwe zamówienia cut-to-pattern (w logach są same dummy),
- kasowanie zamówienia ze SPDE i raport wykonanego zamówienia.

## Bezpieczeństwo

Bez `--enable-plc-writes` nic nie idzie do PLC, kart ani SPDE. Dotyczy to też odczytów z kart, bo każda ramka zaczyna się od SYN, oraz gniazda SPDE, bo przychodzące zamówienie zmienia listę. Wyjątek: `Frm_debug` tylko czyta z PLC.

## Błędy VB zachowane 1:1

Do sprawdzenia na VB i do decyzji, czy poprawiać.

**Najważniejszy: godzina w raporcie produkcji.** `Right$("0" + Trim$(Time), 8)` przy formacie 12-godzinnym zostawia minutę i sekundę (`59.47 PM`). W `Ord_mac_ese.dat` 67 ze 100 wartości czasu ma pierwszą liczbę ≥ 24. Dla nich `DateDiff` kończy się błędem i z kodu wynika, że raport się nie zapisuje.

Karty i konfiguracje:

- `salva_cfg_att`: `res_cfg_att` po dopisaniu rekordu czyści `LIVELLO` także nowej konfiguracji, więc po restarcie program wraca do `DEFAULT`.
- `chg` po `conferma_*` jest liczone po skopiowaniu wartości, więc zmiana prędkości noża nie wymusza ponownego wysłania zamówień.
- `CONFIGURA`: `com_ini` ustawia się po ACK pierwszej części, a wynik pierwszej części jest nadpisywany wynikiem drugiej.
- `Leg_cfg`: strzałka w lewo nie wraca do pierwszych dwóch rekordów.
- `Adj_pat_con`: lista wolnych wartości wysyła „wartość − 1”, więc wolne „1” daje odrzucany kontrast 0.

Ustawienia:

- `Set_par_mac`: walidacja F3 zapisuje do `par_mac` po każdym przejściu, więc anulowanie zostawia część zmian w pamięci.
- `Set_par_mac`: kontrola `nor_mac` czyta `cmb_gra_car`.
- `Set_par_mac`: `And False` sprawia, że zmiana liczby silników nigdy nie idzie przez karty.
- `Set_par_com`: każda parzystość inna niż liczba zapisuje się jako „E”.
- `Set_par_com`: `chk_abi_lan` nie jest odczytywany przy zapisie.
- `Set_par_com`: `porte_ok` wymaga poprawnego portu COM także dla kart po sieci.
- `Abilitazioni`: `msg("ope_incor")` to prawdopodobnie klucz, którego nie ma.

SPDE i pozostałe:

- Kasowanie ze SPDE w czasie zmiany zamówienia odpowiada literą `d` zamiast `e`.
- Każde zamówienie ze SPDE dopisuje lub nadpisuje rekord w `ONDE`.
- `Imp_all`: po ESC zostaje ustawiona flaga `mia_ope`.
- `Pag_lis_ord`: PageDown ustawia wskaźnik na ostatni rekord.
- Różowy kursor ponownie loguje alarmy.
- `Ins_ord`: F4 nie odczytuje ponownie ostatniego pola.
- `Abilitazioni` wysyła stare `par_abi`.
- F9 hasła nadpisuje `Pwd_liv1`.
- Hasła są zapisane prawie otwartym tekstem.

## Świadome różnice względem VB

- Poza edycją pola są tylko do odczytu lub wyłączone. W VB traciły tylko `TabStop`.
- Checkboxy w `Set_par_mac` są poza edycją wyłączone, a nie szare.
- Błąd wykonania w kodzie formularza trafia do logu i program działa dalej (`guardedFormCall`).
- `scr_par_cur_tag_dbf`: gałąź z `LIVELLO0`, która w VB kończy się błędem, tylko loguje.
- `Del_cfg`: F11 bez znalezionego rekordu zamyka dialog zamiast kończyć się błędem.

## Znane problemy na ekranie

- Napisy nie mieszczą się w polach: data w zegarze, etykieta F10, nagłówek `Maks. predkosc`. Oglądane w kontenerze bez czcionek z VB, trzeba sprawdzić na Macu i Windowsie.
- Pasek stanu oglądany bez PLC, więc pola stanu były puste.

## Następne kroki

1. Sprawdzić ekrany na Macu.
2. Zdecydować o poprawce godziny w raporcie produkcji.
3. Zdecydować o pełnym ekranie z rozciąganiem jak w VB. Docelowo program działa na Linuksie, komputer z Windows XP znika. Ruch VB nagrać w Wiresharku, zanim to nastąpi.
4. Uruchomić z `--enable-plc-writes` przy wyłączonej produkcji.
5. Porównać ruch z VB w Wiresharku, najpierw `CONFIGURA` i zamówienia cut-to-pattern.

## Układ formularzy

`vb2qml.py` rysuje rodzeństwo w odwrotnej kolejności niż w `.frm`, bo VB zapisuje kontrolkę wierzchnią jako pierwszą. Tę kolejność wywnioskowano z wyglądu ekranów (`Sta_bar`, `Tasti`), a nie z dokumentacji VB. Jeśli na jakimś ekranie coś jest zasłonięte, zacząć od porównania z VB.

Świadome odejścia od `.frm`, żeby linie i paski były całe: ukryte wąskie białe etykiety bez tekstu (`is_white_filler` w generatorze), białe kawałki w `Tasti` i `Sta_bar`, wyśrodkowane klawisze F, `scrap` do krawędzi paska.

Układ wypełnia całe okno: szerokość i wysokość skalują się osobno, jak `PercX` i `PercY` w VB. Przy proporcjach innych niż 4:3 tekst i okrągłe lampki są rozciągnięte. Minimalny rozmiar okna to 640×480.

## Języki

`Sel_lin` oferuje włoski, angielski, niemiecki i polski (`lin_att` 0, 1, 2, 8). Inne wartości `Lin_att` w `stag.ini` nadal działają, ale nie ma ich na liście.

- Teksty idą najpierw z `MESSAGGI` w bazie z maszyny. Baza nie jest zmieniana.
- Tam, gdzie język jest pusty albo jest kopią włoskiego, działa nakładka `src/core/translations.inc`. Gdy i tam nie ma tekstu, zostaje włoski zamiast pustego pola.
- Napisy z formularzy, których nie ma w `MESSAGGI`, tłumaczone są przez `Messages::captionText`.
- Zostają bez tłumaczenia: opisy DM w `Frm_debug_des`, formularze CAN, `Stato_IO`, jednostki i nazwy techniczne.
- Nowe tłumaczenia nie były sprawdzane przez osoby z maszyny. Istniejący polski z bazy nie był poprawiany, jest bez polskich znaków. Nowy trzyma ten sam styl.
- Niektóre dłuższe napisy są ucięte w polach, np. `AKTYWNE` jako `AKTY`.

## Analiza statyczna

`cppcheck` 2.14 (`--check-level=exhaustive`, `warning,performance,portability,style`) po poprawkach nie zgłasza żadnego błędu, który psułby działanie. Nieudane zapytania SQL trafiają teraz do logu (`core/sqlcheck.h`). Zostały świadomie:

- Fałszywe alarmy, bo `cppcheck` nie śledzi zmian wewnątrz lambd: `finsclient.cpp`, `tst_miscpages.cpp`.
- Warunki i wartości przeniesione 1:1 z VB: `orderdialogmodel.cpp`, `MAX_NUM_ORD` i `MAX_NUM_ORD_ESE` w `collezioneordini.cpp`.
- Kosmetyka: nazwy `liv` i `vel` w `classestato.cpp` jak w VB, pętle zamiast algorytmów STL.

ASan/UBSan nie były uruchamiane, bo nie linkują się tutaj z musl. Trzeba je puścić na Macu.

## Budowanie i testy

```sh
cmake -S stag-qt -B <katalog_budowania>
cmake --build <katalog_budowania> -j8
<katalog_budowania>/bin/stag
cd <katalog_budowania>
QT_QPA_PLATFORM=offscreen QT_QUICK_BACKEND=software ctest -j4
```

Budowanie kopiuje do `bin/` bazę `stag.sqlite` i dane maszyny do `bin/dati/` (`*.ini`, `*.dat`, `*.txt`). Kopiuje tylko to, czego tam nie ma, więc przebudowanie nie nadpisuje zmian zrobionych przez program. Żeby wrócić do danych z maszyny, usuń te pliki i zbuduj ponownie.

`stag` bez argumentów bierze dane z `bin/`, a gdy ich tam nie ma, z bieżącego katalogu. `--data` i `--db` mają pierwszeństwo.

Pliki generowane są pilnowane testami. Po zmianie kodu VB uruchom ponownie:

- `tools/vb2qml.py` (formularze QML),
- `tools/gen_schtag_tables.py` (`schtagtables.inc`),
- `tools/gen_debug_des.py` (`debugdes.inc`).

Testy nie mogą zmieniać `stag/` ani `stag-qt/stag.sqlite`. Po ich uruchomieniu `git status --short stag stag-qt/stag.sqlite` powinien być pusty.
