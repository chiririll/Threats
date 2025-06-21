# Диплом в Markdown

Попытка написать диплом без использования _Microsoft Word_ (спойлер: не получилось) и _LaTeX_ (потому что слишком сложно, а я ленивый).

Сборка _docx_ происходит с использованием [_Pandoc_][pandoc] через _GitHub Actions_. Для локальной сборки использовался [_Docker_][docker] + [_Act_][act] и расширение [GitHub Local Actions][gh-loc-act] для [VSCode][vscode]

В качестве основы для документа используется файл [_PZ.docx_](docs/templates/PZ.docx). Список источников оформляется не правильно (за исключением страниц сайта), поэтому использовались ссылки только на электронные источники. Однако, при желании, можно исправить файл [_gost.csl_](docs/templates/gost.csl) для корректного оформления других типов источников.

Презентация сделана без с использованием [_Marp_][marp]. Для удобства использовалось расширение [_Marp for VS Code_][marp-vsc]. Сборка также производилась через _GitHub Actions_.

## Структура репозитория

- **/Data/** - Данные для работы программы
    - **Input/** - Входные данные для парсера (необработанные
    - **Include/** - Файлы, которые будут встроены в exe файл программы _Threats.exe_ (базы данных нарушителей, угроз и т.д.)
- **/docs/** - Сам диплом (пояснительная записка), разделенный на файлы по главам
    - **bibliography.bib** - Список источников в формате _BibLaTex_
    - **meta.yml** - Настройки для _Pandoc_
    - **pipe/** - Модель сети Петри, разработанная в среде [_PIPE 4.3_][pipe]
    - **images/** - Изображения для пояснительной записки
    - **filters/** - Lua фильтры для _Pandoc_
        - **bullet-lists.lua** - Замена абзацев списка на обычные, с ручным добавлением маркера
        - **latin-italic.lua** - Выделение латинских символов курсивом во всем документе
    - **presentation/** - Презентация для защиты, сделаная в [_Marp_][marp]
    - **templates/** - Шаблон _docx_ файла и стили для списка источников
- **/Threats/** - C# проект GUI приложения диплома
- **/Threats.Data/** - C# проект контейнеров данных
- **/Threats.Parser/** - C# проект парсера входных данных для их преобразования в программный формат

[pandoc]: https://pandoc.org/
[docker]: https://www.docker.com/
[act]: https://nektosact.com/
[gh-loc-act]: https://marketplace.visualstudio.com/items?itemName=SanjulaGanepola.github-local-actions
[vscode]: https://code.visualstudio.com/
[pipe]: https://sourceforge.net/projects/pipe2/
[marp]: https://marpit.marp.app/
[marp-vsc]: https://marketplace.visualstudio.com/items?itemName=marp-team.marp-vscode