
namespace Billder.Application.Custom
{
    public class Paginacion<T>
    {
        public List<T> Trabajos { get; set; }
        public int CantidadTotal { get; set; }
        public int PaginaActual { get; set; }
        public int PageSize { get; set; }
        public int TotalDePaginas { get; set; }
        public bool HasPrevious => PaginaActual > 1;
        public bool HasNext => PaginaActual < TotalDePaginas;
    }
}
