using Project_Prn.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; } //  Thêm khóa ngoại đến Course

    public DateOnly IssuedDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public string? CertificateCode { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Course Course { get; set; } = null!; //  Navigation property
}
