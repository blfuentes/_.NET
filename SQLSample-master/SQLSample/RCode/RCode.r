install.packages("RODBC")
library("RODBC")
odbcChannel <- odbcConnect("TestDB")

#sqlFetch(odbcChannel, "FileSample")
#  Error in odbcQuery(channel, query, rows_at_time) :
#  'Calloc' could not allocate memory (214748364800 of 1 bytes)
#  Además: Warning messages:
#  1: In odbcQuery(channel, query, rows_at_time) :
#  Reached total allocation of 8075Mb: see help(memory.size)
#  2: In odbcQuery(channel, query, rows_at_time) :
#  Reached total allocation of 8075Mb: see help(memory.size)

dataFrame <- sqlQuery(odbcChannel, "select myfile = convert(varchar(max), FileData)  from FileSample where FileId = 1")
write.csv(dataFrame, file = "MyData.csv")