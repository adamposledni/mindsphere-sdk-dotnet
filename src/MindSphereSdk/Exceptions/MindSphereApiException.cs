﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MindSphereSdk.Exceptions
{
    public class MindSphereApiException : Exception
    {
        public MindSphereApiException(string message) : base(message)
        {
            
        }
    }
}