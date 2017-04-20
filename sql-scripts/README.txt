After creating empty db 'bookcountry' on localhost you can create tables and fill them with mock data 
by running all the scripts in this folder (sql-scripts) with command line:

FOR %A IN ("*.sql") DO "your_path_to_mysql_server" -h localhost --user=Your_Username --password=Your_Password bookcountry < %A >output.tab