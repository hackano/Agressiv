## Agress.ExcelCreator

Contains the beginnings of a project that writes all reported hours to an excel sheet.

## Agress.Logic

Contains the reflected Agresso pages that are being automated, and some WatiN driver classes.

Also contains a class `Credentials`. The default implementation of this will

1. Read from a file called `credentials.txt` next to the executable if it exists. Its format is found below.
2. Read from the environment, two variables: `AGRESSO_USERNAME` and `AGRESSO_PASSWORD`.

## Agress.Logic.Specs

Specifications trying all pages out - making sure they work and making sure that the API hasn't changed.

## Agress.Messages

Contains all message declarations that are used to communicate beween clients and web drivers.

## Agress.UI

Original ugly WebForm GUI.

## Agress.WebDriver

A backend web driver, hosted in TopShelf

## `credentials.txt` format

```
username
password
```