using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models.Testx
{
  [Table("Category", Schema = "dbo")]
  public partial class Category
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId
    {
      get;
      set;
    }

    public ICollection<User> Users { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
