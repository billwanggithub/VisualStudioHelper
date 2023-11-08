<script src='http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML' type='text/javascript'>
    MathJax.Hub.Config({  
     TeX: { equationNumbers: { autoNumber: "AMS" } },  
     tex2jax: {  
      inlineMath: [ ['$','$'], ["\\(","\\)"] ],  
      displayMath: [ ['$$','$$'], ["\\[","\\]"] ],  
      processEscapes: true },  
     'HTML-CSS': { scale: 90 },  
     displayIndent: '2em'  
    });  
</script>

<script src="https://cdn.jsdelivr.net/npm/mermaid/dist/mermaid.min.js">
    mermaid.initialize({ startOnLoad: true });
</script>

[TOC]

# MarkDownHelper

## Install From NuGet

- [Leisn.MarkdigToc](https://github.com/leisn/MarkdigToc)
- [Markdig](https://github.com/xoofx/markdig)

![Markdownhelper](Assets/markdownhelper.png)

## Project reference

- InternetHelper

## Usage

```csharp
Helper.MarkDownHelper.OpenUserGuideFromLocal("UserGuide.md");
```

## Add TOC

Add `[TOC]` on top to Markdown file

```console
[TOC]
# t1
## t1.1
### t1.1.1
### t1.1.2
## t1.2
```

## FlowChart(Mermaid)

- Use Mermaid to write flowchart in markdown
    The following code will look like this

    ```c
    flowchart TD
    A[Start] --> B{Is it?}
    B -->|Yes| C[OK]
    C --> D[Rethink]
    D --> B
    B ---->|No| E[End]
    ```

    ```mermaid

flowchart TD
    A[Start] --> B{Is it?}
    B -->|Yes| C[OK]
    C --> D[Rethink]
    D --> B
    B ---->|No| E[End]
    ```

- It doesn't look like Mermaid is built into Markdig. Rather, Markdig leaves the source unmodified.Add the follwoing code to MarkDown

    ```javascript
    <script src="https://cdn.jsdelivr.net/npm/mermaid/dist/mermaid.min.js"></script>
    <script>
    mermaid.initialize({ startOnLoad: true });
    </script>
    ```

## Math Support(Latex)

- Use LaTeX to write equation

    The following example will generate the math equation

    ```latex
    When $a \ne 0$, there are two solutions to $(ax^2 + bx + c = 0)$ and they are 
    $$ x = {-b \pm \sqrt{b^2-4ac} \over 2a} $$
    ```

    When $a \ne 0$, there are two solutions to $(ax^2 + bx + c = 0)$ and they are
    $$ x = {-b \pm \sqrt{b^2-4ac} \over 2a} $$

- Add the following code to Top of Markdown file

```javascript
  <script src='http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML' type='text/javascript'>
    MathJax.Hub.Config({  
     TeX: { equationNumbers: { autoNumber: "AMS" } },  
     tex2jax: {  
      inlineMath: [ ['$','$'], ["\\(","\\)"] ],  
      displayMath: [ ['$$','$$'], ["\\[","\\]"] ],  
      processEscapes: true },  
     'HTML-CSS': { scale: 90 },  
     displayIndent: '2em'  
    });  
</script>
```

### Reference

- [Math support in Markdown](https://github.blog/2022-05-19-math-support-in-markdown/)
- [使用 MathJax 把 LaTeX 或 MathML 數學式子放進網頁](https://blog.gtwang.org/web-development/mathjax-latex-mathml/)
- [使用 MathJax 函式庫顯示 Latex 語法](https://www.mropengate.com/2015/04/blogger-mathjax-latex.html)
- [MathJax: 让前端支持数学公式](https://www.cnblogs.com/geyouneihan/p/9743302.html)
- [用 MathJax 顯示數學符號－以 Blogger 為例](https://note-on-cat.blogspot.com/2013/07/mathjax-blogger.html)
- [Latex设置字体大小](https://www.jianshu.com/p/ad400d7fe885)
- [How draw diagram with Markdown](https://stackoverflow.com/questions/70631812/how-draw-diagram-with-markdown)
- [Mermain Tutorual](https://mermaid.js.org/config/Tutorials.html)
