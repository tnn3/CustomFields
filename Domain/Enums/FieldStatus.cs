﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum FieldStatus
    {
        Hidden, //field will not be shown in create/edit
        Disabled, //unable to change value after create
        Active
    }
}
