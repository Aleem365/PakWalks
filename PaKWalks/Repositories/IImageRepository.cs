using Domain_OverView.Models.Domain_Model;
using System.Net;

namespace PaKWalks.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
