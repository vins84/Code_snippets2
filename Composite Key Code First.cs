https://msdn.microsoft.com/en-us/library/jj591583%28v=vs.113%29.aspx?f=255&MSPPError=-2147217396

http://www.codeproject.com/Articles/813912/Create-Primary-Key-using-Entity-Framework-Code-Fir

http://www.entityframeworktutorial.net/code-first/foreignkey-dataannotations-attribute-in-code-first.aspx

http://stackoverflow.com/questions/14873169/creating-composite-key-entity-framework


// ====================   Composite Key   =================================

//*******************   You can use either fluent API   ******************* 
public class Category
{
    public int CategoryId1 { get; set; }
    public int CategoryId2 { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int CategoryId1 { get; set; }
    public int CategoryId2 { get; set; }

    public virtual Category Category { get; set; }
}

public class Context : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasKey(c => new {c.CategoryId1, c.CategoryId2});

        modelBuilder.Entity<Product>()
            .HasRequired(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => new {p.CategoryId1, p.CategoryId2});

    }
}


//*******************   Or data annotations:   ******************* 

public class Category
{
    [Key, Column(Order = 0)]
    public int CategoryId2 { get; set; }
    [Key, Column(Order = 1)]
    public int CategoryId3 { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    [ForeignKey("Category"), Column(Order = 0)]
    public int CategoryId2 { get; set; }
    [ForeignKey("Category"), Column(Order = 1)]
    public int CategoryId3 { get; set; }

    public virtual Category Category { get; set; }
}


//=================================     Composite key   and    Foreign Composite Key  - GOOD  =================================

The model
I’ll demonstrate code first DataAnnotations with a simple pair of classes: Blog and Post.
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BloggerName { get; set;}
        public virtual ICollection<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

As they are, the Blog and Post classes conveniently follow code first convention and required no tweaks to help EF work with them. But you can also use the annotations to provide more information to EF about the classes and the database that they map to.
Key
Entity Framework relies on every entity having a key value that it uses for tracking entities. One of the conventions that code first depends on is how it implies which property is the key in each of the code first classes. That convention is to look for a property named “Id” or one that combines the class name and “Id”, such as “BlogId”. The property will map to a primary key column in the database.
The Blog and Post classes both follow this convention. But what if they didn’t? What if Blog used the name PrimaryTrackingKey instead or even foo? If code first does not find a property that matches this convention it will throw an exception because of Entity Framework’s requirement that you must have a key property. You can use the key annotation to specify which property is to be used as the EntityKey.
    public class Blog
    {
        [Key]
        public int PrimaryTrackingKey { get; set; }
        public string Title { get; set; }
        public string BloggerName { get; set;}
        public virtual ICollection<Post> Posts { get; set; }
    }

If you are using code first’s database generation feature, the Blog table will have a primary key column named PrimaryTrackingKey which is also defined as Identity by default.
jj591583_figure01
Composite keys
Entity Framework supports composite keys - primary keys that consist of more than one property. For example, your could have a Passport class whose primary key is a combination of PassportNumber and IssuingCountry.
    public class Passport
    {
        [Key]
        public int PassportNumber { get; set; }
        [Key]
        public string IssuingCountry { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
    }

If you were to try and use the above class in your EF model you wuld get an InvalidOperationExceptions stating;
Unable to determine composite primary key ordering for type 'Passport'. Use the ColumnAttribute or the HasKey method to specify an order for composite primary keys.
When you have composite keys, Entity Framework requires you to define an order of the key properties. You can do this using the Column annotation to specify an order.
Note: The order value is relative (rather than index based) so any values can be used. For example, 100 and 200 would be acceptable in place of 1 and 2.
    public class Passport
    {
        [Key]
        [Column(Order=1)]
        public int PassportNumber { get; set; }
        [Key]
        [Column(Order = 2)]
        public string IssuingCountry { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expires { get; set; }
    }

If you have entities with composite foreign keys then you must specify the same column ordering that you used for the corresponding primary key properties.
Only the relative ordering within the foreign key properties needs to be the same, the exact values assigned to Order do not need to match. I.e. in the following example 3 and 4 could be used in place of 1 and 2.
    public class PassportStamp
    {
        [Key]
        public int StampId { get; set; }
        public DateTime Stamped { get; set; }
        public string StampingCountry { get; set; }

        [ForeignKey("Passport")]
        [Column(Order = 1)]
        public int PassportNumber { get; set; }

        [ForeignKey("Passport")]
        [Column(Order = 2)]
        public string IssuingCountry { get; set; }

        public Passport Passport { get; set; }
    }

Required
The Required annotation tells EF that a particular property is required.
Adding Required to the Title property will force EF (and MVC) to ensure that the property has data in it.
    [Required]
    public string Title { get; set; }

With no additional no code or markup changes in the application, an MVC application will perform client side validation, even dynamically building a message using the property and annotation names.