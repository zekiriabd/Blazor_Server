using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models.Testx
{
  [Table("Role", Schema = "dbo")]
  public partial class Role
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id
    {
      get;
      set;
    }
    public int UserId
    {
      get;
      set;
    }
    public User User { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
