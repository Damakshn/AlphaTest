# Введение

## Общее описание
Проект представляет собой систему онлайн-тестирования для проверки знаний учащихся в рамках дистанционного обучения. Система должна обеспечивать возможность гибкого подхода к составлению тестовых заданий и проверке работ, с тем чтобы разные образовательные организации, преподаватели, организаторы учебных курсов и т.д. могли адаптировать составление и проверку тестов под своё видение учебного процесса.

Ниже представлена контекстная диаграмма системы в нотации модели C4:

![Контекстная диаграмма](/docs/Images/c4_context_diagram.svg)

Диаграмма контекста показывает систему "в общем", фокусируясь не на конкретных деталях, а на акторах, задействованных в процессе - какие классы пользователей взаимодействуют с системой, с какими внешними сервисами общается сама система.
По мере развития проекта диаграмма может меняться и дополняться.

## Цель создания системы
Создание данного проекта преследует две цели:
 - **учебная** - научиться строить архитектуру веб-приложения, написанного на C# с использованием принципов чистой архитектуры и предметно-ориентированного проектирования (DDD);
 - **демонстрационная** - показать свои навыки в проектировании и разработке;

## Границы проекта
Система тестирования задумана и будет реализовываться именно как система для проверки знаний в онлайн-режиме. Сложные подходы к проверке работ, такие как деление на первичные и тестовые баллы (как в ЕГЭ) реализовывать не планируется. Также за рамками проекта остаются всевозможные статистические опросы, а также тесты со сложными системами метрик, например, психологическое тестирование.

Дерево функций системы представлено на рисунке ниже:

![Дерево функций](/docs/Images/feature_tree.svg)

Узлы, помеченные оранжевым, подлежат уточнению. По мере развития проекта перечень функций, включённых в границы проекта, может дополняться и корректироваться.

# Требования к системе

## Классы пользователей и их характеристики
|     Класс пользователей     |                    Описание                    |
|-----------------------------|------------------------------------------------|
|Преподаватель|Сотрудник организации, непосредственно осуществляющий обучение и проверку знаний учащихся. Преподаватели составляют тесты, проводят экзамены, проверяют работы, выставляют оценки. Также преподаватель может выступать в качестве куратора учебной группы и иметь доступ к отчётам об успеваемости.|
|Администратор|Сотрудник организации, осуществляющий контрольные функции. Администраторы управляют учётными записями пользователей, они отвечают за предоставление доступа к системе, блокировку пользователей, создание учебных групп. В особых случаях администратор может вмешиваться в процедуру проведения экзаменов и составления тестов.|
|Учащийся (студент)|Пользователь, чьи знания подлежат проверке посредством тестирования в системе AlphaTest. Учащиеся объединены в учебные группы, в которых они сдают тесты на оценку.|

## Варианты использования
Для различных категорий пользователей определены следующие варианты использования:

|  Действующее лицо  |       Вариант использования       |
|--------------------|-----------------------------------|
|**_Преподаватель_**||
||**Составление тестов**|
||Просмотр списка тестов|
||Создание теста|
||Изменение названия или темы теста|
||Просмотр меню настроек теста|
||Изменение порядка прохождения вопросов теста|
||Настройка возможности повторно отправлять ответы на вопросы|
||Задание лимита времени на тест|
||Изменение числа попыток сдачи теста|
||Выбор политики оценивания ответов в тесте|
||Задание проходного балла для сдачи теста|
||Отправка заявки на публикацию теста|
||Просмотр списка вопросов в тесте|
||Добавление вопроса в тест|
||Удаление вопроса из теста|
||Изменение порядка вопросов в тесте|
||Просмотр информации о вопросе в тесте|
||Редактирование вопроса|
||Отправка теста в архив|
||Создание новой редакции теста|
||Настройка способа проверки работ|
||Настройка распределения баллов за вопросы|
||**Управление доступом к тесту**|
||Добавление другого преподавателя в список составителей|
||Исключение другого преподавателя из списка составителей|
||Передача теста другому преподавателю|
||**Проведение экзаменов**|
||Создание (назначение) экзамена|
||Перенос сроков экзамена|
||Отмена экзамена|
||**Проверка работ учащихся**|
||Просмотр списка непроверенных работ|
||Просмотр списка непроверенных ответов в сданном тесте|
||Проверка работы экзаменатором|
||Ручная оценка ответа на вопрос|
|**_Администратор_**||
||**Управление учётными записями**|
||Регистрация новой учётной записи|
||Массовый импорт сведений об учащихся|
||Назначение ролей доступа для пользователей|
||Блокировка учётной записи|
||Повторная генерация временного пароля|
||Передача теста другому автору|
||Передача полномочий экзаменатора|
||**Работа с учебными группами**|
||Создание учебной группы|
||Добавление учащихся в группу|
||Исключение учащихся из группы|
||Назначение преподавателя куратором группы|
||Расформирование учебной группы|
||Редактирование учебной группы|
||**Обработка заявок**|
||Обработка заявки на публикацию теста|
||Рассмотрение заявки на публикацию теста|
|**_Учащийся_**||
||Просмотр списка новых экзаменов|
||Просмотр информации о тесте|
||Начало тестирования|
||Просмотр списка вопросов в тесте|
||Просмотр информации о вопросе|
||Переход между вопросами|
||Отправка ответа на вопрос|
||Отзыв отправленноо ответа на вопрос|
||Досрочное завершение тестирования|
||Просмотр списка результатов тестирования|
||Просмотр детальной информации об индивидуальных результатах тестирования|
|**_Все пользователи_**||
||Смена пароля|
|**_Автоматические операции_**||
||Автоматическая проверка работ|
||Блокировка учётной записи пользователя по истечении срока действия временного пароля|

Подробное описание вариантов использования [здесь](/docs/UseCases.md)
