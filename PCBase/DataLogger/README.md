![Beijer Logo](https://mb.cision.com/Public/668/logo/80ef19c951201062_org.jpg)

# Development Environment

Integrated Development Environment :  iX Developer 2.41.177.0

Project Target : PC

Additional Controls: DataGridView, DateTimePicker, ListView

# Purpose

Sample Script of Datalogger usage. 

In this sample project the default name of datalogger is 'DataLogger1' and the default name of  history datalogger is 'DataLogger2', adjust your script according to actual conditions.

```
static string dataLogger = "DataLogger1";
static string dataLogger_history = "DataLogger2";
```



# Features

- Basic
  - Start Log
    - Enable the datalogger.
  - Stop Log
    - Disable the datalogger.
  - Log Once
    - Record a data.
- DataLogger Query
  - Query All
    - Show the all data in datalogger.
  - Query by Time
    - Show the data In the time range.
  - History Data
    - Show the all data what you import.
- Export 
  - Export Setting
    - Choose the datalogger files export path and file naming function.
  - Export Columns Setting
    - Setting the column name of query result and export files.
  - Export DataLogger
- Import
  - Import CSV Files
    - According to export setting path to load all CSV files in th folder.

