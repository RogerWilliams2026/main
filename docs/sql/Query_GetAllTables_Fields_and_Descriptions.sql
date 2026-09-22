

SELECT
   sys.tables.name AS table_name,
   sys.tables.object_id,
   sys.columns.name AS ColumnName, sys.columns.system_type_id AS DataType, sys.columns.max_length AS MAXLength, 
   sys.extended_properties.value AS Description

   FROM sys.tables
   INNER JOIN sys.columns ON sys.tables.object_id = sys.columns.object_id
   INNER JOIN sys.extended_properties ON sys.columns.object_id = sys.extended_properties.major_id
                                      AND sys.columns.column_id = sys.extended_properties.minor_id
WHERE sys.tables.type_desc = 'USER_TABLE'
ORDER BY sys.tables.name;

     