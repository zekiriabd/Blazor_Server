using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models.Testx
{
  [Table("User", Schema = "dbo")]
  public partial class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId
    {
      get;
      set;
    }

    public ICollection<Role> Roles { get; set; }
    public string FirstName
    {
      get;
      set;
    }
    public string LastName
    {
      get;
      set;
    }
    public Int16 Age
    {
      get;
      set;
    }
    public int? CategoryId
    {
      get;
      set;
    }
    public Category Category { get; set; }
  }
}
