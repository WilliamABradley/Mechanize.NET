# Mechanize.NET

Stateful programmatic web browsing, based on Python-Mechanize, which is based on Andy Lester’s Perl module WWW::Mechanize. 

## Example

### Google Search

```C#
var page = await browser.NavigateAsync("https://www.google.com/");
if (page.IsHtml)
{
    var form = page.Forms["f"];
    var queryfield = form.FindControl("q");
    queryfield.Value = "Mechanize.NET";

    var newpage = await form.SubmitForm();
    var contents = newpage.Document;
    // Collect the Results from contents.
}
```

## Build status



## The Mechanize Family

| Language | Creators | Name |
| ------ | ------ | ------ |
| Perl | Andy Lester | [WWW::Mechanize](http://search.cpan.org/~petdance/WWW-Mechanize/) |
| Python | Kovid Goyal | [Python-Mechanize](https://pypi.python.org/pypi/mechanize/) |
| Java | GistLabs | [Mechanize for Java](https://github.com/GistLabs/mechanize) |
| C# | William Bradley | [Mechanize.NET](https://github.com/WilliamABradley/Mechanize.NET) |