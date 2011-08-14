using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;    
using NHibernate;
using NHibernate.UserTypes;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Core.Persistence.NH.Types
{
public class BooleanM1 :  IUserType
{
    public bool IsMutable
    {
        get { return false; }
    }

    public Type ReturnedType
    {
        get { return typeof(bool); }//{ return typeof(BooleanM1); }
    }

    public SqlType[] SqlTypes
    {
        get { return new[]{NHibernateUtil.Int16.SqlType}; }
    }

    public object NullSafeGet(IDataReader rs, string[] names, object owner)
    {
        var obj = NHibernateUtil.String.NullSafeGet(rs, names[0]);

        if(obj == null ) return false;

        return ((string)obj == "-1" || (string)obj == "1") ? true : false;
    }

    public void NullSafeSet(IDbCommand cmd, object value, int index)
    {
        if(value == null)
        {
            ((IDataParameter) cmd.Parameters[index]).Value = DBNull.Value;
        }
        else
        {
          ((IDataParameter) cmd.Parameters[index]).Value = (bool)value ? -1 : 0;
        }
    }

    public object DeepCopy(object value)
    {
        return value;
    }

    public object Replace(object original, object target, object owner)
    {
        return original;
    }

    public object Assemble(object cached, object owner)
    {
        return cached;
    }

    public object Disassemble(object value)
    {
        return value;
    }

    public new bool Equals(object x, object y)
    {
        if( ReferenceEquals(x,y) ) return true;

        if( x == null || y == null ) return false;

        return x.Equals(y);
    }

    public int GetHashCode(object x)
    {
        return x == null ? typeof(bool).GetHashCode() + 473 : x.GetHashCode();
    }
}}
