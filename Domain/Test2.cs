﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Test.Domain
{
  public class Test2
  {
    virtual public long         ID          { get; set; }
    virtual public string       Description { get; set; }
    virtual public TestCategory Category    { get; set; }
  }
}
