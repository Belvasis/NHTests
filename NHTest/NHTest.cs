using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace Core.Test
{

  using Domain;

  [TestFixture]
  public class NHTest
  {
    ISessionFactory pFactory;

    [SetUp]
    void SetUp()
    {
      Configuration pConfig = new Configuration().Configure("nhtest.config");
      pConfig.AddAssembly(Assembly.GetExecutingAssembly());
      pFactory = pConfig.BuildSessionFactory();
    }

    [Test]
    public void Test_NotFound_Ignore()
    {
      using (var Session = pFactory.OpenSession())
      { 
        Assert.DoesNotThrow(() => 
            {
              var pList = Session.Query<Test>().ToList();
            });
        Assert.DoesNotThrow(() => 
            {
              var pList = Session.Query<Test2>().ToList();
            });
      }    
    }
  }
}
