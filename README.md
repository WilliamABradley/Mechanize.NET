# Mechanize.NET

Stateful programmatic web browsing, based on Python-Mechanize, which is based on Andy Lester’s Perl module WWW::Mechanize. 

[![license](https://img.shields.io/github/license/williamabradley/Mechanize.NET.svg)](https://github.com/WilliamABradley/Mechanize.NET/blob/master/LICENSE)
[![AppVeyor](https://img.shields.io/appveyor/ci/WilliamABradley/mechanize-net.svg)](https://ci.appveyor.com/project/WilliamABradley/mechanize-net)
[![NuGet](https://img.shields.io/nuget/v/Mechanize.NET.svg)](https://www.nuget.org/packages/Mechanize.NET/)
[![NuGet](https://img.shields.io/nuget/dt/Mechanize.NET.svg)](https://www.nuget.org/packages/Mechanize.NET/)

## Example

### Google Search

```C#
var page = await browser.NavigateAsync("https://www.google.com/");
if (page.IsHtml)
{
    var form = page.Forms["f"];
    var queryfield = form.FindControl<ScalarControl>("q");
    queryfield.Value = "Mechanize.NET";

    var newpage = await form.SubmitForm();
    var contents = newpage.Document;
    // Collect the Results from contents.
}
```

## The Mechanize Family

| Language | Creators | Name |
| ------ | ------ | ------ |
| Perl | Andy Lester | [WWW::Mechanize](http://search.cpan.org/~petdance/WWW-Mechanize/) |
| Python | Kovid Goyal | [Python-Mechanize](https://pypi.python.org/pypi/mechanize/) |
| Ruby | SparkleMotion | [Mechanize](https://github.com/sparklemotion/mechanize) |
| Java | GistLabs | [Mechanize for Java](https://github.com/GistLabs/mechanize) |
| C# | William Bradley | [Mechanize.NET](https://github.com/WilliamABradley/Mechanize.NET) |