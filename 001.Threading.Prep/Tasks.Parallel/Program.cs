using Tasks.Parallel;

// var testDb = new TestDb(new BloggingContext());
// testDb.VerifyDbOperations();

//var dataMigration = new DataMigration(new BloggingContext());

var taskParallel = new TaskParallel(new BloggingContext());
taskParallel.DoWork();