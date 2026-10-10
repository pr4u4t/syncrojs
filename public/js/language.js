(() => {
  let pickerObserver;
  const translations = {
    'Syncro Orders': 'Zlecenia Syncro',
    'Syncro TCP': 'Syncro TCP',
    'Home': 'Strona główna',
    'Home dashboard': 'Pulpit',
    'Home Dashboard': 'Pulpit',
    'Review active production orders': 'Przeglądaj aktywne zlecenia produkcyjne',
    'Browse completed production history': 'Przeglądaj historię zakończonej produkcji',
    'Live status': 'Status bieżący',
    'Shift Overview': 'Podsumowanie zmiany',
    'Output': 'Produkcja',
    '3 order levels': '3 poziomy zleceń',
    'Level 0': 'Poziom 0',
    'Level 1': 'Poziom 1',
    'Level 2': 'Poziom 2',
    'Operations': 'Operacje',
    'Materials': 'Materiały',
    'History': 'Historia',
    'Application': 'Aplikacja',
    'Connecting': 'Łączenie',
    'Import Orders': 'Import zleceń',
    'Shift Overview': 'Podsumowanie zmiany',
    'Start Shift': 'Rozpocznij zmianę',
    'Production operations': 'Panel produkcyjny',
    'Live status': 'Status na żywo',
    'Live': 'Na żywo',
    'Update delayed': 'Opóźniona aktualizacja',
    'Waiting for update': 'Oczekiwanie na aktualizację',
    'Updated': 'Aktualizacja',
    's ago': 's temu',
    'Live operator data could not be read.': 'Nie udało się odczytać bieżącego statusu operatora.',
    'Active alarms and warnings': 'Aktywne alarmy i ostrzeżenia',
    'Unknown machine': 'Nieznana maszyna',
    'Output': 'Produkcja',
    'Shift ID': 'ID zmiany',
    'Stop Time': 'Godzina zatrzymania',
    'Required': 'Wymagane',
    'Good': 'Dobre',
    'Bad': 'Wadliwe',
    'Meters produced': 'Wyprodukowane metry',
    'Dry meters required': 'Wymagane metry suche',
    'Wet-end meters': 'Metry mokrego końca',
    'Wet-end required': 'Wymagane metry mokrego końca',
    'Max speed': 'Prędkość maksymalna',
    'AOC speed': 'Prędkość AOC',
    'Target speed': 'Prędkość docelowa',
    'Dry setup': 'Ustawienie suchego końca',
    'Dry setup started': 'Rozpoczęcie ustawienia suchego końca',
    'Wet-end started': 'Rozpoczęcie mokrego końca',
    'Line status': 'Status linii',
    'Line speed': 'Prędkość linii',
    'Dry meters left': 'Pozostałe metry suche',
    'Wet-end meters left': 'Pozostałe metry mokrego końca',
    'Setups in queue': 'Ustawienia w kolejce',
    'Dry setup progress': 'Postęp ustawienia suchego końca',
    'AOC mode': 'Tryb AOC',
    'AOC type': 'Typ AOC',
    'AOC time left': 'Pozostały czas AOC',
    'Simulation': 'Symulacja',
    'First stop:': 'Pierwszy postój:',
    'Level 0': 'Poziom 0',
    'Level 1': 'Poziom 1',
    'Level 2': 'Poziom 2',
    '3 order levels': '3 poziomy zleceń',
    'Loading live production data...': 'Ładowanie danych produkcji...',
    'Review active production orders': 'Przeglądaj aktywne zlecenia produkcyjne',
    'Browse completed production history': 'Przeglądaj historię zakończonej produkcji',
    'Past': 'Archiwum',
    'Show': 'Lista',
    'Export': 'Eksport',
    'Current': 'Bieżące',
    'Import': 'Import',
    'Shift': 'Zmiana',
    'Shift List': 'Lista zmian',
    'Shift Start': 'Rozpoczęcie zmiany',
    'Start': 'Rozpocznij',
    'Stop': 'Zakończ',
    'Papers': 'Papiery',
    'Quality': 'Jakość',
    'Flute': 'Fala',
    'Settings': 'Ustawienia',
    'Language': 'Język',
    'Cache maintenance': 'Pamięć podręczna',
    'Clear cached query results. This does not delete records from the database.': 'Wyczyść tymczasowe wyniki zapytań. Ta operacja nie usuwa rekordów z bazy danych.',
    'Clear cache': 'Wyczyść cache',
    'Cached query results cleared. Database records were not changed.': 'Wyczyszczono cache zapytań. Rekordy w bazie danych nie zostały zmienione.',
    'Settings saved in the database. Some changes require a service restart.': 'Ustawienia zapisano w bazie danych. Część zmian wymaga ponownego uruchomienia usługi.',
    'true': 'tak',
    'false': 'nie',
    'Current Orders': 'Bieżące zlecenia',
    'Order file parser': 'Parser plików zleceń',
    'Classic DAT / CSV optimizer': 'Klasyczny DAT / optymalizator CSV',
    'Extended orders (CSV, up to 3 orders)': 'Rozszerzone zlecenia (CSV, maks. 3)',
    'CSV: one row per order; rows sharing setup_code are grouped into one setup. Supports up to 3 orders and 22 crease measurements total. Put measurements in score_measures_mm, separated by semicolons. Existing fixed-width DAT files remain supported, with 5 crease fields per order.': 'CSV: jeden wiersz na zamówienie; wiersze z tym samym setup_code tworzą jedno ustawienie. Obsługuje maksymalnie 3 zamówienia i 22 pomiary bigów łącznie. Pomiary wpisz w score_measures_mm, oddzielając je średnikami. Pliki DAT o stałej szerokości nadal są obsługiwane, z 5 polami bigów na zamówienie.',
    'Classic DAT parser. CSV files are optimized into the classic fixed-width DAT layout before preview and transfer.': 'Klasyczny parser DAT. Pliki CSV są optymalizowane do układu DAT o stałej szerokości przed podglądem i wysłaniem.',
    'Server': 'Serwer',
    'Orders': 'Zlecenia',
    'App': 'Aplikacja',
    'Cache': 'Pamięć podręczna',
    'Log': 'Dziennik',
    'Web width': 'Szerokość wstęgi',
    'Setup length': 'Długość ustawienia',
    'Trim type': 'Typ odpadu',
    'Trim': 'Odpad',
    'Position': 'Pozycja',
    'Address': 'Adres',
    'Score type': 'Typ bigowania',
    'Current Order Import': 'Import bieżących zleceń',
    'Order entry methods': 'Metody wprowadzania zleceń',
    'Import file': 'Import z pliku',
    'Enter manually': 'Wprowadź ręcznie',
    'Manual setup': 'Ręczne wprowadzanie ustawienia',
    'Setup code': 'Kod ustawienia',
    'Order count': 'Liczba zamówień',
    'Crease measures (total)': 'Pomiary bigów (łącznie)',
    'Unique setup code generated automatically. Edit if needed.': 'Unikalny kod ustawienia generuje się automatycznie; możesz go zmienić.',
    'Calculated trim': 'Wyliczony odpad',
    'Enter web width, sheet widths and outs to calculate trim.': 'Wpisz szerokość wstęgi, szerokości arkuszy i użytki, aby wyliczyć odpad.',
    'Use positive widths with at most one decimal place and outs from 1 to 5.': 'Szerokości muszą być dodatnie (maks. jedno miejsce po przecinku), a użytki od 1 do 5.',
    'Crease measurements must be non-negative with at most one decimal place.': 'Pozycje bigów muszą być nieujemne i mieć maksymalnie jedno miejsce po przecinku.',
    'Crease measurements cannot exceed the web width.': 'Pozycje bigów nie mogą przekraczać szerokości wstęgi.',
    'Used width exceeds web width.': 'Wykorzystana szerokość przekracza szerokość wstęgi.',
    'Calculated trim exceeds configured limit.': 'Wyliczony odpad przekracza skonfigurowany limit.',
    'Trim is within configured limits.': 'Odpad mieści się w skonfigurowanym limicie.',
    'Width used:': 'Wykorzystana szerokość:',
    'Web width (mm)': 'Szerokość wstęgi (mm)',
    'Quality ID': 'ID jakości',
    'Select quality': 'Wybierz jakość',
    'Flute ID': 'ID fali',
    'Select flute': 'Wybierz falę',
    'Orders in setup': 'Liczba zleceń w ustawieniu',
    'Order 1': 'Zlecenie 1',
    'Order 2': 'Zlecenie 2',
    'Order 3': 'Zlecenie 3',
    'Level': 'Poziom',
    'Upper': 'Góra',
    'Lower': 'Dół',
    'Order number': 'Numer zlecenia',
    'Customer': 'Klient',
    'Sheet width (mm)': 'Szerokość arkusza (mm)',
    'Outs': 'Liczba użytków',
    'Sheet length (mm)': 'Długość arkusza (mm)',
    'Quantity (sheets)': 'Liczba arkuszy',
    'Sheets per stack': 'Arkuszy w stosie',
    'Score 1 (mm)': 'Linia bigowania 1 (mm)',
    'Score 2 (mm)': 'Linia bigowania 2 (mm)',
    'Score 3 (mm)': 'Linia bigowania 3 (mm)',
    'Score 4 (mm)': 'Linia bigowania 4 (mm)',
    'Score 5 (mm)': 'Linia bigowania 5 (mm)',
    'Creasing lines (mm)': 'Linie bigowania (mm)',
    'One crease measurement per line. Maximum 22 across the setup.': 'Jeden pomiar bigowania w każdym wierszu; maksymalnie 22 na całe ustawienie.',
    'crease measures entered': 'bigów wprowadzonych',
    'A setup may contain no more than 22 crease measures': 'Ustawienie może zawierać maksymalnie 22 pomiary bigowania.',
    'Send setup to Syncro': 'Wyślij ustawienie do Syncro',
    'Sending...': 'Wysyłanie...',
    'Setup': 'Ustawienie',
    'accepted by Syncro. Trim:': 'zaakceptowano w Syncro. Odpad:',
    'Quality and flute lists could not be loaded from the database.': 'Nie udało się pobrać list jakości i fal z bazy danych.',
    'No quality or flute IDs are available in the database.': 'W bazie danych brak dostępnych ID jakości lub fali.',
    'Past Orders': 'Zakończone zlecenia',
    'Past Order Export': 'Eksport zakończonych zleceń',
    'Past Order List': 'Lista zakończonych zleceń',
    'Paper List': 'Lista papierów',
    'Quality List': 'Lista jakości',
    'Flute List': 'Lista fal',
    'Dry End': 'Suchy koniec',
    'Order Level 0': 'Poziom zlecenia 0',
    'Order Level 1': 'Poziom zlecenia 1',
    'Order Level 2': 'Poziom zlecenia 2',
    'Required:': 'Wymagane:',
    'Good:': 'Dobre:',
    'Bad:': 'Wadliwe:',
    'Go to current': 'Przejdź do bieżących',
    'Go to past': 'Przejdź do archiwum',
    'Back to Home': 'Wróć do strony głównej',
    'Add': 'Dodaj',
    'View and import current orders': 'Wyświetl i importuj bieżące zlecenia',
    'View and export past orders': 'Wyświetl i eksportuj zakończone zlecenia',
    'Clear Orders': 'Wyczyść zlecenia',
    'Go to date': 'Przejdź do daty',
    'Export past orders as CSV': 'Eksportuj zakończone zlecenia do CSV',
    'Export past orders as XLSX': 'Eksportuj zakończone zlecenia do XLSX',
    'Print/Save as PDF': 'Drukuj/Zapisz jako PDF',
    'Print / Save as PDF': 'Drukuj / Zapisz jako PDF',
    'Export CSV': 'Eksportuj CSV',
    'Export XLSX': 'Eksportuj XLSX',
    'Submit': 'Wyślij',
    'Save': 'Zapisz',
    'Accept': 'Akceptuj',
    'Deny': 'Odrzuć',
    'Cancel': 'Anuluj',
    'Back': 'Wstecz',
    'New Shift': 'Nowa zmiana',
    'unknown': 'nieznana',
    'Shift:': 'Zmiana:',
    'Manager': 'Kierownik',
    'People': 'Liczba osób',
    'Orders Import Confirm': 'Potwierdzenie importu zleceń',
    'Orders Validation Result': 'Wynik walidacji zleceń',
    'Failed to parse order:': 'Nie udało się odczytać zlecenia:',
    'unused': 'nieużywane',
    'Summary:': 'Podsumowanie:',
    'Total/Success/Failed:': 'Razem/Poprawne/Błędne:',
    'Orders Adding': 'Dodawanie zleceń',
    'Orders are being added': 'Trwa dodawanie zleceń',
    'Processing order list': 'Przetwarzanie listy zleceń',
    'Success': 'Sukces',
    'Orders were transfered successfully': 'Zlecenia zostały przesłane',
    'Failed': 'Błąd',
    'Orders transfer failed at:': 'Przesyłanie zleceń nie powiodło się przy:',
    'Next ›': 'Dalej ›',
    'Last »': 'Ostatnia »',
    '« First': '« Pierwsza',
    '‹ Previous': '‹ Poprzednia',
    'Previous': 'Poprzednia',
    'Next': 'Następna',
    'Pagination': 'Paginacja',
    'Quick access': 'Szybki dostęp',
    'Production metrics': 'Wskaźniki produkcji',
    'Installed software versions': 'Wersje zainstalowanego oprogramowania',
    'Machine readiness': 'Gotowość maszyn',
    'Loading...': 'Ładowanie...',
    'Loading records...': 'Ładowanie danych...',
    'No records found.': 'Brak rekordów.',
    'No current orders found.': 'Brak bieżących zleceń.',
    'Database connection unavailable. Could not load records.': 'Brak połączenia z bazą danych. Nie udało się pobrać rekordów.',
    'Database connection unavailable. Export rows could not be loaded.': 'Brak połączenia z bazą danych. Nie udało się pobrać danych eksportu.',
    'Syncro connection unavailable. Home data could not be loaded.': 'Brak połączenia z Syncro. Nie udało się pobrać danych pulpitu.',
    'Syncro TCP is reachable, but dashboard data could not be read.': 'Połączenie TCP z Syncro jest dostępne, ale nie udało się pobrać danych pulpitu.',
    'Syncro TCP is reachable, but current order data could not be read.': 'Połączenie TCP z Syncro jest dostępne, ale nie udało się pobrać bieżących zleceń.',
    'Syncro connection unavailable. Current orders could not be loaded.': 'Brak połączenia z Syncro. Nie udało się pobrać bieżących zleceń.',
    'Could not load records.': 'Nie udało się pobrać danych.',
    'Retry': 'Ponów',
    'Checking': 'Sprawdzam',
    'Connected': 'Połączono',
    'Disconnected': 'Brak połączenia',
    'Unavailable': 'Niedostępne',
    'Connection status': 'Status połączeń',
    'Syncro is unavailable. Shift data could not be loaded.': 'Syncro jest niedostępne. Nie udało się pobrać danych zmiany.',
    'Settings sections': 'Sekcje ustawień',
    'Settings could not be loaded. Refresh the page to try again.': 'Nie udało się wczytać ustawień. Odśwież stronę i spróbuj ponownie.',
    'Warning: some changes may require a manual service restart.': 'Uwaga: niektóre zmiany mogą wymagać ręcznego ponownego uruchomienia usługi.',
    'Warning some of the changes may require manual service restart from terminal': 'Uwaga: niektóre zmiany mogą wymagać ręcznego ponownego uruchomienia usługi w terminalu.',
    'Order file input': 'Plik zleceń',
    'Choose an order file first.': 'Najpierw wybierz plik ze zleceniami.',
    'The CSV optimizer service is unavailable.': 'Usługa optymalizacji CSV jest niedostępna.',
    'The order file could not be read.': 'Nie udało się odczytać pliku ze zleceniami.',
    'Could not read the current Syncro queue; no setup was sent.': 'Nie udało się odczytać bieżącej kolejki Syncro; ustawienie nie zostało wysłane.',
    'Syncro rejected the manual setup.': 'Syncro odrzuciło ręczne ustawienie.',
    'Could not cancel the import.': 'Nie udało się anulować importu.',
    'Could not start import.': 'Nie udało się rozpocząć importu.',
    'Syncro queue is unavailable. The order result is still unknown.': 'Kolejka Syncro jest niedostępna. Wynik wysyłki nadal jest nieznany.',
    'Could not check order status.': 'Nie udało się sprawdzić statusu zlecenia.',
    'Check order status': 'Sprawdź status zlecenia',
    'Go to Orders': 'Przejdź do zleceń',
    'Toggle navigation': 'Przełącz nawigację',
    'ID ': 'ID ',
    'Start Time': 'Godzina rozpoczęcia',
    'End Time': 'Godzina zakończenia',
    'StartTime': 'Godzina rozpoczęcia',
    'FinishTime': 'Godzina zakończenia',
    'RunTime': 'Czas pracy',
    'StopTime': 'Czas postoju',
    'NumberOfStop': 'Liczba postojów',
    'SetupChanges': 'Zmiany ustawień',
    'QualityChanges': 'Zmiany jakości',
    'ShiftManager': 'Kierownik zmiany',
    'AvgSpeed': 'Średnia prędkość',
    'Job': 'Zlecenie',
    'Comm': 'Uwagi',
    'SEQ': 'Sekwencja',
    'SETUP LENGTH': 'Długość ustawienia',
    'ORDER 1': 'ZLECENIE 1',
    'ORDER 2': 'ZLECENIE 2',
    'ORDER 3': 'ZLECENIE 3',
    'finishtime': 'Czas zakończenia',
    'produced0': 'Wyprodukowane arkusze 1'
  };

  Object.assign(translations, {
    'Emergency': 'Awaria',
    'Alarm': 'Alarm',
    'Warning': 'Ostrzeżenie',
    'Ready': 'Gotowa',
    'Not ready': 'Niegotowa',
    'Ready to run': 'Gotowa do pracy',
    'Not ready to run': 'Niegotowa do pracy',
    'Past Order Export': 'Eksport zakończonych zleceń',
    'Print / Save as PDF': 'Drukuj / Zapisz jako PDF',
    'Export CSV': 'Eksportuj CSV',
    'Export XLSX': 'Eksportuj XLSX',
    'Server': 'Serwer',
    'Orders': 'Zlecenia',
    'App': 'Aplikacja',
    'Cache': 'Pamięć podręczna',
    'Log': 'Dziennik',
    'Settings sections': 'Sekcje ustawień',
    'Settings could not be loaded. Refresh the page to try again.': 'Nie udało się wczytać ustawień. Odśwież stronę i spróbuj ponownie.',
    'Warning: some changes may require a manual service restart.': 'Uwaga: niektóre zmiany mogą wymagać ręcznego ponownego uruchomienia usługi.',
    'Stop shift': 'Zakończ zmianę',
    'Actions': 'Operacje',
    'Action': 'Akcja',
    'Remove setup': 'Usuń ustawienie',
    'Could not remove this setup.': 'Nie udało się usunąć ustawienia.',
    'Edit': 'Edytuj',
    'Edit material': 'Edytuj materiał',
    'Close': 'Zamknij',
    'Yes': 'Tak',
    'No': 'Nie',
    'Cancel': 'Anuluj',
    'Save changes': 'Zapisz zmiany',
    'Saving...': 'Zapisywanie...',
    'This material was changed or removed. Reload the list and try again.': 'Ten materiał został zmieniony lub usunięty. Odśwież listę i spróbuj ponownie.',
    'A record with these key values already exists.': 'Rekord o takich wartościach klucza już istnieje.',
    'Could not save this material. Check the values and database connection.': 'Nie udało się zapisać materiału. Sprawdź wartości i połączenie z bazą danych.',
    'Crease measures': 'Pomiary bigów',
    'idle': 'nieaktywna',
    'working': 'w trakcie pracy',
    'Could not start shift. Check the entered values and Syncro connection.': 'Nie udało się rozpocząć zmiany. Sprawdź wpisane dane i połączenie z Syncro.',
    'Setup code already exists in Syncro.': 'Kod ustawienia już istnieje w Syncro.',
    'No final acknowledgement for setup': 'Brak końcowego potwierdzenia dla ustawienia',
    'Connection lost while sending setup': 'Utracono połączenie podczas wysyłania ustawienia',
    'Syncro has not confirmed setup': 'Syncro nie potwierdziło ustawienia',
    'Syncro rejected setup': 'Syncro odrzuciło ustawienie',
    'Connection lost while waiting for setup': 'Utracono połączenie podczas oczekiwania na ustawienie',
    'Setup is not yet visible in the current Syncro queue. Its result is still unknown.': 'Ustawienia nie widać jeszcze w bieżącej kolejce Syncro. Wynik pozostaje nieznany.',
    'Orders were transferred successfully.': 'Zlecenia zostały pomyślnie wysłane.',
    'Processing order list...': 'Przetwarzanie listy zleceń...',
    'Waiting for Syncro acknowledgement for setup': 'Oczekiwanie na potwierdzenie Syncro dla ustawienia',
    'Current order data could not be loaded.': 'Nie udało się wczytać bieżących zleceń.',
    'Web width': 'Szerokość wstęgi',
    'Setup length': 'Długość ustawienia',
    'Trim type': 'Typ odpadu',
    'Trim': 'Odpad',
    'Position': 'Pozycja',
    'Address': 'Adres',
    'Score type': 'Typ bigowania',
    'finishtime': 'Czas zakończenia',
    'produced0': 'Wyprodukowane arkusze 1',
    'produced1': 'Wyprodukowane arkusze 2',
    'produced2': 'Wyprodukowane arkusze 3',
    'ordernumber0': 'Numer zlecenia 1',
    'ordernumber1': 'Numer zlecenia 2',
    'ordernumber2': 'Numer zlecenia 3',
    'customername0': 'Klient 1',
    'customername1': 'Klient 2',
    'customername2': 'Klient 3',
    'outs0': 'Użytki 1',
    'outs1': 'Użytki 2',
    'outs2': 'Użytki 3',
    'width0': 'Szerokość arkusza 1',
    'width1': 'Szerokość arkusza 2',
    'width2': 'Szerokość arkusza 3',
    'length0': 'Długość arkusza 1',
    'length1': 'Długość arkusza 2',
    'length2': 'Długość arkusza 3',
    'scoremeasures0': 'Bigi 1',
    'scoremeasures1': 'Bigi 2',
    'scoremeasures2': 'Bigi 3',
    'paperid': 'ID papieru',
    'addedbydownload': 'Dodano automatycznie',
    'maxsplicedbspeed_uvm_m': 'Maks. prędkość łączenia DB (m/min)',
    'maxsplicesf1linerspeed_uvm_m': 'Maks. prędkość łączenia linera SF1 (m/min)',
    'maxsplicesf1flutingspeed_uvm_m': 'Maks. prędkość łączenia flutingu SF1 (m/min)',
    'maxsplicesf2linerspeed_uvm_m': 'Maks. prędkość łączenia linera SF2 (m/min)',
    'maxsplicesf2flutingspeed_uvm_m': 'Maks. prędkość łączenia flutingu SF2 (m/min)',
    'maxsplicesf3linerspeed_uvm_m': 'Maks. prędkość łączenia linera SF3 (m/min)',
    'maxsplicesf3flutingspeed_uvm_m': 'Maks. prędkość łączenia flutingu SF3 (m/min)',
    'corediameter_uddmm': 'Średnica tulei (mm)',
    'weightfactor_uwg_m': 'Współczynnik gramatury (g/m²)',
    'defaultthickness_uddmmm': 'Grubość domyślna (mm)',
    'text': 'Opis',
    'lastused': 'Ostatnio użyty',
    'paperclassification': 'Klasyfikacja papieru',
    'qualityid': 'ID jakości',
    'qualitytext': 'Opis jakości',
    'bundlelayersperstack': 'Warstwy pakietu w stosie',
    'weightfactoruwg_m': 'Współczynnik gramatury (g/m²)',
    'bundlereversemode': 'Odwrócony układ pakietu',
    'preprint': 'Nadruk',
    'inclinatelastbelt': 'Nachylenie ostatniej taśmy',
    'paper1id': 'ID papieru 1',
    'paper1weightfactor_uwg_m': 'Gramatura papieru 1',
    'paper2id': 'ID papieru 2',
    'paper2weightfactor_uwg_m': 'Gramatura papieru 2',
    'paper3id': 'ID papieru 3',
    'paper3weightfactor_uwg_m': 'Gramatura papieru 3',
    'paper4id': 'ID papieru 4',
    'paper4weightfactor_uwg_m': 'Gramatura papieru 4',
    'paper5id': 'ID papieru 5',
    'paper5weightfactor_uwg_m': 'Gramatura papieru 5',
    'paper6id': 'ID papieru 6',
    'paper6weightfactor_uwg_m': 'Gramatura papieru 6',
    'paper7id': 'ID papieru 7',
    'paper7weightfactor_uwg_m': 'Gramatura papieru 7',
    'flute': 'Fala',
    'sfsposition': 'Pozycja SF',
    'flutetext': 'Opis fali',
    'scoregap_uddmm': 'Odstęp między bigami (mm)',
    'grindingparameter_udm': 'Parametr szlifowania',
    'lubricationparameter_udm': 'Parametr smarowania',
    'trimgrindingsmultiplier': 'Mnożnik szlifowania odpadu',
    'grindingdistance_udm': 'Dystans szlifowania',
    'lubricationtime_utds': 'Czas smarowania',
    'productionspeed_uvm_m': 'Prędkość produkcji (m/min)',
    'aocspeed_uvm_m': 'Prędkość AOC (m/min)',
    'cruisingspeed_uvm_m': 'Prędkość robocza (m/min)',
    'thickness_uddmm': 'Grubość (mm)',
    'terminalflutecode': 'Końcowy kod fali',
    'twin400internalflapdisabled': 'Wewnętrzna klapa Twin400 wyłączona'
  });

  const dynamicTranslations = [
    { pattern: /^Setup code (.+) already exists in Syncro\.$/, translate: (match) => `Kod ustawienia ${match[1]} już istnieje w Syncro.` },
    { pattern: /^No final acknowledgement for setup (.+)\. Check status before retrying\.$/, translate: (match) => `Brak końcowego potwierdzenia dla ustawienia ${match[1]}. Sprawdź jego status przed ponowną próbą.` },
    { pattern: /^Connection lost while sending setup (.+)\. Check status before retrying\.$/, translate: (match) => `Utracono połączenie podczas wysyłania ustawienia ${match[1]}. Sprawdź status przed ponowną próbą.` },
    { pattern: /^Syncro has not confirmed setup (.+)\. Check queue status before retrying\.$/, translate: (match) => `Syncro nie potwierdziło ustawienia ${match[1]}. Sprawdź kolejkę przed ponowną próbą.` },
    { pattern: /^Syncro rejected setup (.+), or its setup code already exists\.$/, translate: (match) => `Syncro odrzuciło ustawienie ${match[1]} lub jego kod już istnieje.` },
    { pattern: /^Connection lost while waiting for setup (.+)\. Check status before retrying\.$/, translate: (match) => `Utracono połączenie podczas oczekiwania na ustawienie ${match[1]}. Sprawdź status przed ponowną próbą.` },
    { pattern: /^Setup (.+) still has an unconfirmed result\. Check its status before uploading again\.$/, translate: (match) => `Wynik ustawienia ${match[1]} nie jest jeszcze potwierdzony. Sprawdź jego status przed ponownym importem.` },
    { pattern: /^Setup (.+) has an unconfirmed result\. Check its status before cancelling\.$/, translate: (match) => `Wynik ustawienia ${match[1]} jest niepotwierdzony. Sprawdź jego status przed anulowaniem.` },
    { pattern: /^Setup (.+) is not yet visible in the current Syncro queue\. Its result is still unknown\.$/, translate: (match) => `Ustawienia ${match[1]} nie widać jeszcze w bieżącej kolejce Syncro. Wynik pozostaje nieznany.` },
    { pattern: /^Shift: idle$/, translate: () => 'Zmiana: nieaktywna' },
    { pattern: /^Shift: working$/, translate: () => 'Zmiana: w trakcie pracy' }
  ];

  Object.assign(translations, {
    'produced1': 'Wyprodukowane arkusze 2',
    'produced2': 'Wyprodukowane arkusze 3',
    'ordernumber0': 'Numer zlecenia 1',
    'ordernumber1': 'Numer zlecenia 2',
    'ordernumber2': 'Numer zlecenia 3',
    'customername0': 'Klient 1',
    'customername1': 'Klient 2',
    'customername2': 'Klient 3',
    'outs0': 'Użytki 1',
    'outs1': 'Użytki 2',
    'outs2': 'Użytki 3',
    'width0': 'Szerokość arkusza 1',
    'width1': 'Szerokość arkusza 2',
    'width2': 'Szerokość arkusza 3',
    'length0': 'Długość arkusza 1',
    'length1': 'Długość arkusza 2',
    'length2': 'Długość arkusza 3',
    'scoremeasures0': 'Bigi 1',
    'scoremeasures1': 'Bigi 2',
    'scoremeasures2': 'Bigi 3',
    'paperid': 'ID papieru',
    'addedbydownload': 'Dodano automatycznie',
    'maxsplicedbspeed_uvm_m': 'Maks. prędkość łączenia DB (m/min)',
    'maxsplicesf1linerspeed_uvm_m': 'Maks. prędkość łączenia linera SF1 (m/min)',
    'maxsplicesf1flutingspeed_uvm_m': 'Maks. prędkość łączenia flutingu SF1 (m/min)',
    'maxsplicesf2linerspeed_uvm_m': 'Maks. prędkość łączenia linera SF2 (m/min)',
    'maxsplicesf2flutingspeed_uvm_m': 'Maks. prędkość łączenia flutingu SF2 (m/min)',
    'maxsplicesf3linerspeed_uvm_m': 'Maks. prędkość łączenia linera SF3 (m/min)',
    'maxsplicesf3flutingspeed_uvm_m': 'Maks. prędkość łączenia flutingu SF3 (m/min)',
    'corediameter_uddmm': 'Średnica tulei (mm)',
    'weightfactor_uwg_m': 'Współczynnik gramatury (g/m²)',
    'defaultthickness_uddmmm': 'Grubość domyślna (mm)',
    'text': 'Opis',
    'lastused': 'Ostatnio użyty',
    'paperclassification': 'Klasyfikacja papieru',
    'qualityid': 'ID jakości',
    'qualitytext': 'Opis jakości',
    'bundlelayersperstack': 'Warstwy pakietu w stosie',
    'weightfactoruwg_m': 'Współczynnik gramatury (g/m²)',
    'bundlereversemode': 'Odwrócony układ pakietu',
    'preprint': 'Nadruk',
    'inclinatelastbelt': 'Nachylenie ostatniej taśmy',
    'paper1id': 'ID papieru 1',
    'paper1weightfactor_uwg_m': 'Gramatura papieru 1',
    'paper2id': 'ID papieru 2',
    'paper2weightfactor_uwg_m': 'Gramatura papieru 2',
    'paper3id': 'ID papieru 3',
    'paper3weightfactor_uwg_m': 'Gramatura papieru 3',
    'paper4id': 'ID papieru 4',
    'paper4weightfactor_uwg_m': 'Gramatura papieru 4',
    'paper5id': 'ID papieru 5',
    'paper5weightfactor_uwg_m': 'Gramatura papieru 5',
    'paper6id': 'ID papieru 6',
    'paper6weightfactor_uwg_m': 'Gramatura papieru 6',
    'paper7id': 'ID papieru 7',
    'paper7weightfactor_uwg_m': 'Gramatura papieru 7',
    'flute': 'Fala',
    'sfsposition': 'Pozycja SF',
    'flutetext': 'Opis fali',
    'scoregap_uddmm': 'Odstęp między bigami (mm)',
    'grindingparameter_udm': 'Parametr szlifowania',
    'lubricationparameter_udm': 'Parametr smarowania',
    'trimgrindingsmultiplier': 'Mnożnik szlifowania odpadu',
    'grindingdistance_udm': 'Dystans szlifowania',
    'lubricationtime_utds': 'Czas smarowania',
    'productionspeed_uvm_m': 'Prędkość produkcji (m/min)',
    'aocspeed_uvm_m': 'Prędkość AOC (m/min)',
    'cruisingspeed_uvm_m': 'Prędkość robocza (m/min)',
    'thickness_uddmm': 'Grubość (mm)',
    'terminalflutecode': 'Końcowy kod fali',
    'twin400internalflapdisabled': 'Wewnętrzna klapa Twin400 wyłączona'
  });

  const originalText = new WeakMap();
  const lastTranslatedText = new WeakMap();
  const originalAttributes = new WeakMap();
  const translatedAttributes = ['aria-label', 'alt', 'placeholder', 'title'];

  const translateTextNode = (node, language) => {
    const displayed = node.nodeValue;
    if (!originalText.has(node) || !lastTranslatedText.has(node) || displayed !== lastTranslatedText.get(node)) {
      originalText.set(node, displayed);
    }

    const source = originalText.get(node);
    const text = source.trim();
    let translated = language === 'pl' ? translations[text] : text;

    if (language === 'pl' && !translated) {
      const dynamic = dynamicTranslations.find(({ pattern }) => pattern.test(text));
      if (dynamic) {
        translated = dynamic.translate(text.match(dynamic.pattern));
      }
    }

    if (language === 'pl' && !translated) {
      const prefix = Object.keys(translations).find((key) => key.endsWith(':') && text.startsWith(key));
      if (prefix) {
        translated = translations[prefix] + text.slice(prefix.length);
      }
    }

    let nextValue = source;
    if (translated) {
      const leading = source.match(/^\s*/)[0];
      const trailing = source.match(/\s*$/)[0];
      nextValue = leading + translated + trailing;
    }
    if (node.nodeValue !== nextValue) {
      node.nodeValue = nextValue;
    }
    lastTranslatedText.set(node, nextValue);
  };

  const translateElement = (element, language) => {
    let originals = originalAttributes.get(element);
    if (!originals) {
      originals = new Map();
      originalAttributes.set(element, originals);
    }

    for (const attribute of translatedAttributes) {
      const value = element.getAttribute(attribute);
      if (value === null && !originals.has(attribute)) {
        continue;
      }
      if (!originals.has(attribute)) {
        originals.set(attribute, value);
      }

      const source = originals.get(attribute);
      element.setAttribute(attribute, language === 'pl' ? translations[source] || source : source);
    }
  };

  const translateSubtree = (root, language) => {
    if (root.nodeType === Node.TEXT_NODE) {
      translateTextNode(root, language);
    } else {
      const walker = document.createTreeWalker(root, NodeFilter.SHOW_TEXT);
      let node;
      while ((node = walker.nextNode())) {
        translateTextNode(node, language);
      }
    }

    if (root.nodeType === Node.ELEMENT_NODE) {
      translateElement(root, language);
    }
    if (root.querySelectorAll) {
      for (const element of root.querySelectorAll('[aria-label], [alt], [placeholder], [title]')) {
        translateElement(element, language);
      }
    }
  };

  const initialize = () => {
    const languageSwitch = document.getElementById('language-picker');
    if (!languageSwitch) {
      if (!pickerObserver) {
        pickerObserver = new MutationObserver(() => {
          if (document.getElementById('language-picker')) {
            pickerObserver.disconnect();
            pickerObserver = null;
            initialize();
          }
        });
        pickerObserver.observe(document.body, { childList: true, subtree: true });
      }
      return;
    }

    let language = 'en';
    try {
      language = localStorage.getItem('syncrojs-language') || 'en';
    } catch {}

    const applyLanguage = (selectedLanguage) => {
      language = selectedLanguage === 'pl' ? 'pl' : 'en';
      document.documentElement.lang = language;
      for (const button of languageSwitch.querySelectorAll('[data-language]')) {
        button.setAttribute('aria-pressed', String(button.dataset.language === language));
      }
      translateSubtree(document.documentElement, language);
      try {
        localStorage.setItem('syncrojs-language', language);
      } catch {}
    };

    languageSwitch.addEventListener('click', (event) => {
      const button = event.target.closest('[data-language]');
      if (button) {
        applyLanguage(button.dataset.language);
      }
    });
    applyLanguage(language);

    const observer = new MutationObserver((records) => {
      for (const record of records) {
        if (record.type === 'characterData') {
          translateTextNode(record.target, language);
          continue;
        }
        for (const node of record.addedNodes) {
          if (node.nodeType === Node.TEXT_NODE) {
            translateTextNode(node, language);
          } else if (node.nodeType === Node.ELEMENT_NODE) {
            translateSubtree(node, language);
          }
        }
      }
    });
    observer.observe(document.body, { childList: true, characterData: true, subtree: true });
  };

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initialize, { once: true });
  } else {
    initialize();
  }
})();