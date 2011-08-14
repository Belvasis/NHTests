using System;
using Iesi.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Test.Domain
{
  public class TestCategory
  {
    public TestCategory()
    {
      Childs  = new HashedSet<TestCategory>();
    }
    virtual public long               ID            { get; set; }
    virtual public TestCategory       Parent        { get; set; }
    virtual public string             Description   { get; set; }
    virtual public ISet<TestCategory> Childs        { get; set; }
    
  }
}
