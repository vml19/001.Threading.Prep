using Microsoft.EntityFrameworkCore;

namespace Tasks.Parallel;

public class DataMigration : IDataMigration
{
    private BloggingContext BloggingContext { get; }

    public DataMigration(BloggingContext bloggingContext)
    {
        BloggingContext = bloggingContext;

        using var db = BloggingContext;

        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

        //Clean up Blogs table
        db.Blogs?.ExecuteDelete();

        // Populate blogs and posts
        var i = 0;
        while (i < 5)
        {
            //Create Blog and Post
            var post1 = new Post()
            {
                Title = $"Post {i}",
                Content = $"Content {i}"
            };

            var post2 = new Post()
            {
                Title = $"Post {i + 1}",
                Content = $"Content {i + 1}"
            };

            var article = new Article()
            {
                ArticleUrl = "http://blogs.msdn.com/adonet",
                Posts = [post1, post2]
            };

            var research = new Research()
            {
                ResearchUrl = "http://blogs.msdn.com/adonet",
                Posts = [post1, post2]
            };

            var miniPost = new MiniPost()
            {
                MiniPostUrl = "http://blogs.msdn.com/adonet",
                Posts = [post1, post2]
            };

            var journalPost = new Journal()
            {
                JournalPost = "http://blogs.msdn.com/adonet",
                Posts = [post1, post2]
            };

            db.Add(article);
            db.Add(research);
            db.Add(miniPost);
            db.Add(journalPost);
            db.SaveChanges();

            i++;
        }

        // Test migration
        if (db.Blogs == null) return;
        foreach (var blog in db.Blogs.Include(blog => blog.Posts))
        {
            var posts = blog.Posts;
            Console.WriteLine($"{blog.BlogId}, {blog.GetType().Name}");
            if (posts == null) continue;
            foreach (var post in posts)
            {
                Console.WriteLine($" ----->> {post.PostId}, {post.Title}, {post.Content}");
            }
        }
    }
}

public interface IDataMigration;