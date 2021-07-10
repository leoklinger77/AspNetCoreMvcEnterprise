using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace Enterprise.App.Extensions
{
    public static class RazorExtension
    {
        public static string FormatDocument(this RazorPage page, int tipoPessoa, string document)
        {
            return tipoPessoa == 1 ? Convert.ToInt64(document).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(document).ToString(@"00\.000\.000\/000\-00");
        }
    }
}
