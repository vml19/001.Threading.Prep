using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Parallel;

public class BloggingContext : DbContext
{
    public BloggingContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "blogging.db");
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<Research> Research { get; set; }
    public DbSet<MiniPost> MiniPost { get; set; }
    public DbSet<Journal> Journal { get; set; }

    public string DbPath { get; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}

public class Blog()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public List<Post>? Posts { get; set; }
}

public class Article : Blog
{
    public string? ArticleUrl { get; set; }
}

public class Research : Blog
{
    public string? ResearchUrl { get; set; }
}

public class MiniPost : Blog
{
    public string? MiniPostUrl { get; set; }
}

public class Journal : Blog
{
    public string? JournalPost { get; set; }
}

public class Post()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public int BlogId { get; init; }
    public Blog? Blog { get; init; }
}