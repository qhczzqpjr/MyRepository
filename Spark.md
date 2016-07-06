# Concept
big data - 
data model - a collection of describe data
schema - describe data using a kind of data model / types of data
table - a collection of rows and columns
columns - name and type
cell -  may or may not have value
DataFrame - memory 
Spark framework - Apache Spark,Spark SQL,Spark Stream,MLlib & ML, GraphX
driver - local program
workers - local threads or cluster  
SparkContext - how and where to access a cluster
SparkContext master models- local, cluster, mesos
# Life Cycle
## Init DataFrame object
### pandas/R
sqlContext.createDataFrame(pandas_df)
### pythonList
sqlContext.createDataFrame(pythonList,['colname1','colname2'])
#### fileSystem
sqlContext.read.text("filesystempath")

List/Spark or pandas DFs/File System-> DataFrames->transformations(filter)-> transformations(ex:select) -> action (ex:show,count)
DataFrame -> fundment, basic element, every in spark is data frame
## transformation
* lazy, only execute when action taken
### column transformation
* df = people.age	
* df = select('*')
* df = select('name','age')
* df = select(df.name, (df.age+10).alias('Newage'))
* df.drop(df.age)
* slen = udf(lambda s:len(s),IntegerType())
  df.select(slen(df.name).alias('slen'))
* df3 = df.filter(df.age > 3)
* df2 = df.distinct()
* df4.select(explode(df4.intlist).alias("anInt")))
* df1 = df.groupBy(df.name).count()
* df.groupBy().avg().collect()
* df.groupBy('name').avg('age','grade'),collect()
** only single for user define the function
    use explode to get a new column
### Row visit method 
* row.name / row['name']

## actions
* cause recipes to be executed in parallel and produce the result
### common actions
* df.show() --row of dataFrame
* df.count() -- groupBy().count() -transformation/ df.count -action
* df.take(1) -- first row as list
* df.collect() -- list
* df.describe(col) -- stat -min,max,sdet,count
* print df.count()
* df.cache() --reuse df
**: warning use collect, get all data leads to outofmemory

# best practise
## Where does the code run - Driver/ Distributed executor / both
* python/scala code in driver
* Transformation in executor 
* Actions in executor and driver
* Use Transformation and actions where it is possible
* Never use collect instead of take in prod
* Use cache for reused dataFrame

# Appendix
Life Cycle is important for learning a new technology, best practise
<http://spark.apache.org/docs/latest/programming-guide.html>
<http://spark.apache.org/docs/latest/api/python/index.html>
<http://spark.apache.org/docs/latest/api/python/pyspark.sql.html>
<http://spark.apache.org/docs/latest/api/scala/>
<https://community.cloud.databricks.com/> 
{gmail,microsoft}