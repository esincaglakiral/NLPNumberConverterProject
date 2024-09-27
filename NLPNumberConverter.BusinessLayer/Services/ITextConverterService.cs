using NLPNumberConverter.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPNumberConverter.BusinessLayer.Services
{
    public interface ITextConverterService
    { 
        // UserText alır ve ConvertedText döndürür
        ConvertedText ConvertText(UserText userText);
    }
}
