# SendEmail

Plain text send email C# example. It will send text from file, not file itself.

Run:
SendEmail.exe path

path - optional path to text file or folder with txt files.
if path in args then will send text from that file or text from all files from folder.

Settings in SendEmail.ini file that should be beside SendEmail.exe file

sender= sender@yandex.ru

recipient= to@gmail.com

copy= copy@yandex.ru; copy2@yandex.ru

email_subject= Message from Quik

smtpServer= smtp.yandex.ru

serverPort= 587

login= login

password= pass

Optional settings in ini file:
EMAIL_TEXT_PATH= 'Path'

if EMAIL_TEXT_PATH set, then will send email from that file or all files from that folder.
if not, then will send text from file "email_text.txt" that should be beside SendEmail.exe file.

EMAIL_TEXT_PATH setting overlap run path arg.
--------------------------------
Отправка текста из файла. Сам файл не отправляется.

Формат запуска:
SendEmail.exe path

path - опциональный аргумент запуска: путь к текстовому файлу или к папке с файлами. Если он указан, то будет отправлен текст из этого файла или все файлы из этой папки.

Настройки указываются в INI файле. Файл должен лежать рядом с запускаемым файлом exe.
В ini файле есть опциональная настройка
EMAIL_TEXT_PATH= 'Path'

Если она указана, то будет отправлен текст из этого файла или все тексты из файлов из этой папки.
Если настройка не указана, то отправляется текст из файла "email_text.txt", который должен лежать рядом с запускаемым файлом exe.

Настройка EMAIL_TEXT_PATH перекрывает аргумент path при запуске программы.
