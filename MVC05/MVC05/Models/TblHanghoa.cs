using System;
using System.Collections.Generic;

namespace MVC05.Models;

public partial class TblHanghoa
{
    public int PkIHanghoaId { get; set; }

    public string STenhang { get; set; } = null!;

    public double FGianiemyet { get; set; }

    public string? SDacdiem { get; set; }

    public string? SXuatxu { get; set; }

    public string? SAnhMinhHoa { get; set; }

    public virtual ICollection<TblChitiethoadon> TblChitiethoadons { get; set; } = new List<TblChitiethoadon>();
}
