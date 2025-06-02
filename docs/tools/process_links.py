#!/usr/bin/env python3

import os
import re
import sys
from collections import defaultdict

# Маппинг классов на их локализованные подписи
CLASS_LABELS = {
    'figure': 'Рисунок',
    'table': 'Таблица',
    'code': 'Листинг',
    # Добавь другие классы при необходимости
}


def collect_references(root_dir):
    ref_dict = {}
    counters = defaultdict(int)  # Счетчики по классам
    pattern = re.compile(r'\{(\w+):([^\}]+)\}')
    image_pattern = re.compile(r'!\[(\w+):([^\s\]]+)(?:\s+([^\]]+))?\]\([^\)]+\)')

    for dirpath, dirnames, filenames in os.walk(root_dir):
        dirnames.sort()   # Сортировка директорий для предсказуемого обхода
        filenames.sort()  # Сортировка файлов
        for filename in filenames:
            if filename.endswith('.md'):
                filepath = os.path.join(dirpath, filename)
                with open(filepath, 'r', encoding='utf-8') as f:
                    content = f.read()

                # Поиск {class:ref}
                for match in pattern.finditer(content):
                    cls, ref = match.group(1), match.group(2)
                    key = f'{cls}:{ref}'
                    if key not in ref_dict:
                        counters[cls] += 1
                        ref_dict[key] = str(counters[cls])

                # Поиск ![class:ref Описание](...)
                for match in image_pattern.finditer(content):
                    cls, ref, _ = match.group(1), match.group(2), match.group(3)
                    key = f'{cls}:{ref}'
                    if key not in ref_dict:
                        counters[cls] += 1
                        ref_dict[key] = str(counters[cls])

    return ref_dict


def replace_references(root_dir, ref_dict):
    object_pattern = re.compile(r'\{(\w+):([^\}]+)\}')
    link_pattern = re.compile(r'\[(\w+):([^\]]+)\]')
    image_pattern = re.compile(r'!\[(\w+):([^\s\]]+)(?:\s+([^\]]+))?\]\(([^\)]+)\)')

    for dirpath, dirnames, filenames in os.walk(root_dir):
        dirnames.sort()
        filenames.sort()
        for filename in filenames:
            if filename.endswith('.md'):
                filepath = os.path.join(dirpath, filename)
                with open(filepath, 'r', encoding='utf-8') as f:
                    content = f.read()

                def object_replacer(match):
                    key = f'{match.group(1)}:{match.group(2)}'
                    return ref_dict.get(key, match.group(0))

                def link_replacer(match):
                    key = f'{match.group(1)}:{match.group(2)}'
                    return ref_dict.get(key, match.group(0))

                def image_replacer(match):
                    cls, ref, caption, path = match.group(1), match.group(2), match.group(3), match.group(4)
                    key = f'{cls}:{ref}'
                    number = ref_dict.get(key, None)
                    label = CLASS_LABELS.get(cls, cls.capitalize())
                    if number:
                        new_caption = f'{label} {number}'
                        if caption:
                            new_caption += f' – {caption}'
                        return f'![{new_caption}]({path})'
                    else:
                        return match.group(0)

                content = object_pattern.sub(object_replacer, content)
                content = link_pattern.sub(link_replacer, content)
                content = image_pattern.sub(image_replacer, content)

                with open(filepath, 'w', encoding='utf-8') as f:
                    f.write(content)


def main():
    if len(sys.argv) < 2:
        print("Использование: script.py <путь_к_директории>")
        sys.exit(1)

    directory = sys.argv[1]
    if not os.path.isdir(directory):
        print(f"Ошибка: '{directory}' — не существует или не директория.")
        sys.exit(1)

    ref_dict = collect_references(directory)
    replace_references(directory, ref_dict)
    print("Нумерация по классам завершена.")


if __name__ == '__main__':
    main()
