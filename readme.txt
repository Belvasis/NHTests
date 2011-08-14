1) not-found="ignore"
=====================
This is the part I don't really understand. Maybe i miss something obvious, but the behaviour is weird.
The model for this test is like this:

Cat1
  |__ Cat2 (Parent = Cat1)
       |
       |____________ Test1
       |____________ Test2
       |
       |____ Cat3 (Parent = Cat2)
               |____ Test3

So, every Test entity may have a Category and every Category may have a parent Category. Since i hvae to
use a legacy database, the relation between a Category and it's parent is nto always valid. A P_CAT_ID = 0
simply means, there is no parent. For this reason i used the mapping attribute not-found="ignore".
If i query for Test entities, i need the category but not the hole Parent category tree. So i thought i
use the lazy="no-proxy" attribute for the parent relationship. The idea was, that the parent will be loaded,
if i really need the concrete parent entity.
For the test, i prepared an SQLLite DB with the following categories:

T_CATEGORY:
===========
CAT_ID  DESCRIPTION P_CAT_ID
1	      testc 1	       0
2	      testc 2	       0
3	      testc 3	       0

Then i have to test tables for the two Test classes:

T_TEST:
=======
TEST_ID CAT_ID DESCRIPTION ACTIVE
  1	     1	     test 1	     0
  2	     0	     test 2	    -1

T_TEST2:
========
TEST_ID CAT_ID DESCRIPTION
  1	     1	     test 1
  2	     0	     test 2
  3      2       test 3

Why two tables? This is the weird part. Test and Test2 are identical but reside in two
different tables to show the behaviour. Both hava a Category property of type TestCategory.
If i query for Test entities:
    var pList = Session.Query<Test>().ToList();
the test passes with no errors.
If i query for Test2 entities:
    var pList = Session.Query<Test2>().ToList();
the test doesn't pass because of the exception:
  "NHibernate.UnresolvableObjectException: No row with the given identifier exists[Core.Test.Domain.TestCategory#0]"

The only difference here is, that the table T_TEST conains 2 records and the table T_TEST2 has one more.
If i remove the lazy="no-proxy" both tests will be successful. 

2) Query for property of IUSerType
==================================
The seconde test fails if the Active property of the Test class is of type BooleanM1, wich is a IUserType
that writes -1 for true values and 0 for false values. The exception:

NHibernate.QueryException: Unable to render boolean literal value [.Where[Core.Test.Domain.Test]
(NHibernate.Linq.NhQueryable`1[Core.Test.Domain.Test], Quote((c, ) => (c.Active)), )] 
---> System.InvalidCastException: 
  Das Objekt des Typs "NHibernate.Type.CustomType" kann nicht in Typ "NHibernate.Type.BooleanType" umgewandelt werden.