import os
import re

def main():
    all_content=""

    regex = re.compile(r" +")

    for i in os.walk('.'):
        if "\\.git\\" in i[0] or "\\bin\\" in i[0] or "\\obj\\" in i[0]:
            continue

        for f in i[2]:
            parts = f.rsplit('.', 1)

            if parts[-1] not in ['xml', 'axaml', 'csproj', 'manifest']:
                continue

            path = os.path.join(i[0], f)
            with open(path, 'r', encoding="UTF-8") as file:
                content = file.read()
                content = regex.sub(' ', content.replace('\n', ''))

            all_content += f"// {f}\n"
            all_content += content + '\n\n'

    with open("source.txt", 'w', encoding="UTF-8") as f:
        f.write(all_content)

if __name__ == "__main__":
    main()

