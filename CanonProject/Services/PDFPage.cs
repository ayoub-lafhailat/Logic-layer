using iText.Layout.Element;

public class PDFPage
{
    private int width;
    private int height;
    private int contentSize;
    private PDFContent content;

    public PDFPage(int width, int height, int contentSize)
    {
        this.width = width;
        this.height = height;
        this.contentSize = contentSize;
        this.content = new PDFContent(contentSize);
    }

    public Image RenderContent()
    {
        return content.GenerateData();
    }
}
