(() => {
  let pickerObserver;
  const translations = {
    'Syncro Orders': 'Zlecenia Syncro',
    'Syncro TCP': 'Syncro TCP',
    'Home': 'Strona główna',
    'Home dashboard': 'Pulpit',
    'Review active production orders': 'Przeglądaj aktywne zlecenia produkcyjne',
    'Browse completed production history': 'Przeglądaj historię zakończonej produkcji',
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
    'Start Shift': 'Rozpocznij zmianę',
    'Production operations': 'Panel produkcyjny',
    'Live status': 'Status na żywo',
    'Live': 'Na żywo',
    'Waiting for update': 'Oczekiwanie na aktualizację',
    'Updated': 'Aktualizacja',
    's ago': 's temu',
    'Live operator data could not be read.': 'Nie udało się odczytać bieżącego statusu operatora.',
    'Active alarms and warnings': 'Aktywne alarmy i ostrzeżenia',
    'Unknown machine': 'Nieznana maszyna',
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
    'Loading live production data...': 'Ładowanie danych produkcji...',
    'Past': 'Archiwum',
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
    'Cache maintenance': 'Zarządzanie pamięcią podręczną',
    'Clear cached query results. This does not delete records from the database.': 'Wyczyść tymczasowe wyniki zapytań. Ta operacja nie usuwa rekordów z bazy danych.',
    'Clear cache': 'Wyczyść pamięć podręczną',
    'Cached query results cleared. Database records were not changed.': 'Wyczyszczono wyniki zapytań z pamięci podręcznej. Rekordy w bazie danych nie zostały zmienione.',
    'Settings saved in the database. Some changes require a service restart.': 'Ustawienia zapisano w bazie danych. Część zmian wymaga ponownego uruchomienia usługi.',
    'true': 'tak',
    'false': 'nie',
    'Current Orders': 'Bieżące zlecenia',
    'Order file parser': 'Parser plików zleceń',
    'Classic DAT / CSV optimizer': 'Klasyczny DAT / optymalizator CSV',
    'Extended orders (CSV, up to 3 orders)': 'Rozszerzone zlecenia (CSV, maks. 3)',
    'CSV: one row per order; rows sharing setup_code are grouped into one setup. Supports up to 3 orders, 15 crease measurements per order and 22 in total. Put measurements in score_measures_mm, separated by semicolons. Existing fixed-width DAT files remain supported, with 5 crease fields per order.': 'CSV: jeden wiersz na zlecenie; wiersze z tym samym setup_code tworzą jedno ustawienie. Obsługuje maksymalnie 3 zlecenia, 15 bigów na zlecenie i 22 łącznie. Bigi wpisz w score_measures_mm, oddzielając je średnikami. Pliki DAT o stałej szerokości nadal są obsługiwane, z 5 polami bigów na zlecenie.',
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
    'Order count': 'Liczba zleceń',
    'Crease measures (total)': 'Bigi (łącznie)',
    'Unique setup code generated automatically. Edit if needed.': 'Unikalny kod ustawienia generuje się automatycznie; możesz go zmienić.',
    'Calculated trim': 'Wyliczony odpad',
    'Enter web width, sheet widths and outs to calculate trim.': 'Wpisz szerokość wstęgi, szerokości arkuszy i użytki, aby wyliczyć odpad.',
    'Use positive widths with at most one decimal place and outs from 1 to 5.': 'Szerokości muszą być dodatnie (maks. jedno miejsce po przecinku), a użytki od 1 do 5.',
    'Crease measurements must be non-negative with at most one decimal place.': 'Bigi muszą być nieujemne i mieć maksymalnie jedno miejsce po przecinku.',
    'Crease measurements cannot exceed the web width.': 'Bigi nie mogą przekraczać szerokości wstęgi.',
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
    'Creasing lines (mm)': 'Linie bigowania (mm)',
    'One crease measurement per line. Maximum 15 per order and 22 across the setup.': 'Jeden big w każdym wierszu; maksymalnie 15 na zlecenie i 22 na całe ustawienie.',
    'crease measures entered': 'bigów wprowadzonych',
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
    'Add': 'Dodaj',
    'Clear Orders': 'Wyczyść zlecenia',
    'Clearing...': 'Czyszczenie...',
    'Go to date': 'Przejdź do daty',
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
    'Summary': 'Podsumowanie',
    'Total': 'Razem',
    'Ready to send': 'Gotowe do wysłania',
    'Skipped': 'Pominięte',
    'Invalid': 'Błędne',
    'Orders are being added': 'Trwa dodawanie zleceń',
    'Processing order list': 'Przetwarzanie listy zleceń',
    'Success': 'Sukces',
    'Failed': 'Błąd',
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
    'Checking': 'Sprawdzanie',
    'Connected': 'Połączono',
    'Disconnected': 'Brak połączenia',
    'Unavailable': 'Niedostępne',
    'Connection status': 'Status połączeń',
    'Syncro is unavailable. Shift data could not be loaded.': 'Syncro jest niedostępne. Nie udało się pobrać danych zmiany.',
    'Settings sections': 'Sekcje ustawień',
    'Settings could not be loaded. Refresh the page to try again.': 'Nie udało się wczytać ustawień. Odśwież stronę i spróbuj ponownie.',
    'Warning: some changes may require a manual service restart.': 'Uwaga: niektóre zmiany mogą wymagać ręcznego ponownego uruchomienia usługi.',
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
    'Start Time': 'Godzina rozpoczęcia',
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
    'Stop shift': 'Zakończ zmianę',
    'Actions': 'Akcje',
    'Action': 'Akcja',
    'Remove setup': 'Usuń ustawienie',
    'Edited': 'Edytowane',
    'Scores changed manually': 'Bigi zmienione ręcznie',
    'Unsaved changes': 'Niezapisane zmiany',
    'No creases': 'Brak bigów',
    'Could not remove this setup.': 'Nie udało się usunąć ustawienia.',
    'Edit': 'Edytuj',
    'Edit material': 'Edytuj materiał',
    'Close': 'Zamknij',
    'Yes': 'Tak',
    'No': 'Nie',
    'Save changes': 'Zapisz zmiany',
    'Saving...': 'Zapisywanie...',
    'This material was changed or removed. Reload the list and try again.': 'Ten materiał został zmieniony lub usunięty. Odśwież listę i spróbuj ponownie.',
    'A record with these key values already exists.': 'Rekord o takich wartościach klucza już istnieje.',
    'Could not save this material. Check the values and database connection.': 'Nie udało się zapisać materiału. Sprawdź wartości i połączenie z bazą danych.',
    'Crease measures': 'Bigi',
    'idle': 'nieaktywna',
    'working': 'w trakcie pracy',
    'Could not start shift. Check the entered values and Syncro connection.': 'Nie udało się rozpocząć zmiany. Sprawdź wpisane dane i połączenie z Syncro.',
    'No final acknowledgement for setup': 'Brak końcowego potwierdzenia dla ustawienia',
    'Connection lost while sending setup': 'Utracono połączenie podczas wysyłania ustawienia',
    'Syncro has not confirmed setup': 'Syncro nie potwierdziło ustawienia',
    'Syncro rejected setup': 'Syncro odrzuciło ustawienie',
    'Connection lost while waiting for setup': 'Utracono połączenie podczas oczekiwania na ustawienie',
    'Setup is not yet visible in the current Syncro queue. Its result is still unknown.': 'Ustawienia nie widać jeszcze w bieżącej kolejce Syncro. Wynik pozostaje nieznany.',
    'Orders were transferred successfully.': 'Zlecenia zostały pomyślnie wysłane.',
    'Processing order list...': 'Przetwarzanie listy zleceń...',
    'Waiting for Syncro acknowledgement for setup': 'Oczekiwanie na potwierdzenie Syncro dla ustawienia',
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
    'corediameter_uddmm': 'Średnica tulei (0,1 mm)',
    'weightfactor_uwg_m': 'Współczynnik gramatury (g/m²)',
    'defaultthickness_uddmmm': 'Grubość domyślna',
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
    'scoregap_uddmm': 'Odstęp między bigami (0,1 mm)',
    'grindingparameter_udm': 'Parametr szlifowania',
    'lubricationparameter_udm': 'Parametr smarowania',
    'trimgrindingsmultiplier': 'Mnożnik szlifowania odpadu',
    'grindingdistance_udm': 'Dystans szlifowania',
    'lubricationtime_utds': 'Czas smarowania',
    'productionspeed_uvm_m': 'Prędkość produkcji (m/min)',
    'aocspeed_uvm_m': 'Prędkość AOC (m/min)',
    'cruisingspeed_uvm_m': 'Prędkość robocza (m/min)',
    'thickness_uddmm': 'Grubość (0,1 mm)',
    'terminalflutecode': 'Końcowy kod fali',
    'twin400internalflapdisabled': 'Wewnętrzna klapa Twin400 wyłączona'
  });

  Object.assign(translations, {
    'Active': 'Aktywna',
    'Inactive': 'Nieaktywna',
    'ready': 'gotowa',
    'not ready': 'niegotowa',
    'at': 'o',
    'Data': 'Dane',
    'Middle': 'Środek',
    'Loading settings...': 'Ładowanie ustawień...',
    'Add material': 'Dodaj materiał',
    'Add record': 'Dodaj rekord',
    'Record details': 'Szczegóły rekordu',
    'Could not check the Syncro queue, so this material was not deleted. Try again when Syncro is connected.': 'Nie udało się sprawdzić kolejki Syncro, więc materiał nie został usunięty. Spróbuj ponownie po połączeniu z Syncro.',
    'Delete': 'Usuń',
    'Record added.': 'Rekord został dodany.',
    'Record deleted.': 'Rekord został usunięty.',
    'Changes saved.': 'Zmiany zostały zapisane.',
    'Could not delete this material. Check the database connection.': 'Nie udało się usunąć materiału. Sprawdź połączenie z bazą danych.',
    'Copy values from': 'Skopiuj wartości z',
    'Choose a record...': 'Wybierz rekord...',
    'Record added. Go back to the import tab; validation refreshes automatically.': 'Rekord został dodany. Wróć do karty importu, walidacja odświeży się automatycznie.',
    'Previous page': 'Poprzednia strona',
    'Next page': 'Następna strona',
    'Refresh orders': 'Odśwież zlecenia',
    'Processing...': 'Przetwarzanie...',
    'Checking...': 'Sprawdzanie...',
    'Submitting...': 'Wysyłanie...',
    'Setup details': 'Szczegóły ustawienia',
    'Setup parameters': 'Parametry ustawienia',
    'Trim (mm)': 'Odpad (mm)',
    'Detailed order fields are not available for this row.': 'Szczegółowe pola zlecenia nie są dostępne dla tego wiersza.',
    'Creasing lines (mm), one per line': 'Bigi (mm), jeden w wierszu',
    'Creases in setup:': 'Liczba bigów w ustawieniu:',
    'Sum of creases:': 'Suma bigów:',
    'Save creases': 'Zapisz bigi',
    'Could not save the creases.': 'Nie udało się zapisać bigów.',
    'Creases saved; the setup passes validation.': 'Bigi zapisane; ustawienie przechodzi walidację.',
    'Creases saved, but the setup still fails validation:': 'Bigi zapisane, ale ustawienie nadal nie przechodzi walidacji:',
    'Could not verify the current Syncro queue; this order was not sent.': 'Nie udało się sprawdzić bieżącej kolejki Syncro; to zlecenie nie zostało wysłane.',
    'Order transfer failed.': 'Przesyłanie zlecenia nie powiodło się.',
    'Page not found': 'Nie znaleziono strony',
    'Page not found.': 'Nie znaleziono strony.',
    'Manual setup fields are required': 'Pola ręcznego ustawienia są wymagane.',
    'Setup code must contain 1-27 letters, digits, underscores, or hyphens': 'Kod ustawienia musi zawierać od 1 do 27 liter, cyfr, podkreśleń lub myślników.',
    'Enter quality, flute, and between one and three orders': 'Podaj jakość, falę oraz od jednego do trzech zleceń.',
    'Quality or flute was not found in the database': 'Nie znaleziono jakości lub fali w bazie danych.'
  });

  const manualFieldName = (name) => name
    .replace(/^Web width$/, 'Szerokość wstęgi')
    .replace(/^Order (\d) sheet width$/, 'Zlecenie $1: szerokość arkusza')
    .replace(/^Order (\d) sheet length$/, 'Zlecenie $1: długość arkusza')
    .replace(/^Order (\d) sheets per stack$/, 'Zlecenie $1: liczba arkuszy w stosie')
    .replace(/^Order (\d) score (\d+)$/, 'Zlecenie $1: big $2')
    .replace(/^Order (\d) outs$/, 'Zlecenie $1: liczba użytków')
    .replace(/^Order (\d) quantity$/, 'Zlecenie $1: liczba arkuszy');

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
    { pattern: /^Shift: working$/, translate: () => 'Zmiana: w trakcie pracy' },
    { pattern: /^Material is used by qualities \((\d+)\): (.+)$/, translate: (match) => `Materiał jest używany przez jakości (${match[1]}): ${match[2]}` },
    { pattern: /^Material is used by setups in the Syncro queue \((\d+)\): (.+)$/, translate: (match) => `Materiał jest używany przez ustawienia w kolejce Syncro (${match[1]}): ${match[2]}` },
    { pattern: /^Not found in the database: (.+)$/, translate: (match) => `Nie znaleziono w bazie danych: ${match[1].replace(/\bpaper /g, 'papier ').replace(/\bflute /g, 'fala ')}` },
    { pattern: /^Setup (\d+)$/, translate: (match) => `Ustawienie ${match[1]}` },
    { pattern: /^Quality (.+) does not exist in the database\. The setup will be sent with this ID\.$/, translate: (match) => `Jakość ${match[1]} nie istnieje w bazie. Ustawienie zostanie wysłane z tym ID.` },
    { pattern: /^Flute (.+) does not exist in the database\. The setup will be sent with this ID\.$/, translate: (match) => `Fala ${match[1]} nie istnieje w bazie. Ustawienie zostanie wysłane z tym ID.` },
    { pattern: /^Add quality (.+) to the database$/, translate: (match) => `Dodaj jakość ${match[1]} do bazy` },
    { pattern: /^Add flute (.+) to the database$/, translate: (match) => `Dodaj falę ${match[1]} do bazy` },
    { pattern: /^The setup has (\d+) outs in total; the limit is (\d+)\.$/, translate: (match) => `Łączna liczba użytków w ustawieniu wynosi ${match[1]}; limit to ${match[2]}.` },
    { pattern: /^Skipped setups \((\d+)\): zero quantity or already in Syncro\. The remaining setups were sent\.$/, translate: (match) => `Pominięto ustawienia (${match[1]}): zerowa ilość albo ustawienie jest już w Syncro. Pozostałe ustawienia zostały wysłane.` },
    { pattern: /^A setup may contain no more than (\d+) crease measures\.?$/, translate: (match) => `Ustawienie może zawierać maksymalnie ${match[1]} bigi.` },
    { pattern: /^Quality (.+) was not found in the database$/, translate: (match) => `Nie znaleziono jakości ${match[1]} w bazie danych.` },
    { pattern: /^Cut trim (.+) mm is outside the configured limit$/, translate: (match) => `Odpad ${match[1]} mm jest poza skonfigurowanym limitem.` },
    { pattern: /^Order (\d) fields are required$/, translate: (match) => `Zlecenie ${match[1]}: pola są wymagane.` },
    { pattern: /^Order (\d) needs an order number \(max 30\) and customer name \(max 15\)$/, translate: (match) => `Zlecenie ${match[1]}: wymagany numer zlecenia (maks. 30 znaków) i nazwa klienta (maks. 15 znaków).` },
    { pattern: /^Order (\d) crease measurements cannot exceed the web width$/, translate: (match) => `Zlecenie ${match[1]}: bigi nie mogą przekraczać szerokości wstęgi.` },
    { pattern: /^Order (\d) may contain no more than (\d+) crease measures$/, translate: (match) => `Zlecenie ${match[1]}: maksymalnie ${match[2]} bigów.` },
    { pattern: /^Order (\d) level must be 1, 2, or 3$/, translate: (match) => `Zlecenie ${match[1]}: poziom musi wynosić 1, 2 lub 3.` },
    { pattern: /^(.+) must be a non-negative millimeter value$/, translate: (match) => `${manualFieldName(match[1])}: wartość w mm nie może być ujemna.` },
    { pattern: /^(.+) must be a positive millimeter value$/, translate: (match) => `${manualFieldName(match[1])}: wartość w mm musi być dodatnia.` },
    { pattern: /^(.+) supports at most one decimal place$/, translate: (match) => `${manualFieldName(match[1])}: maksymalnie jedno miejsce po przecinku.` },
    { pattern: /^(.+) must be a positive integer no greater than (\d+)$/, translate: (match) => `${manualFieldName(match[1])}: wymagana dodatnia liczba całkowita nie większa niż ${match[2]}.` }
  ];

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