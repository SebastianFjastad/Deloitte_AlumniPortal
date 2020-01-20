using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniPortal.Models
{
    public enum FileType
    {
        ArticleImage = 1,
        ProfilePic = 2,
        AlbumCover = 3,
        AlbumImage = 4,
        Special = 5
    }

    public enum RoleType
    {
        Admin = 1, 
        ContentAdmin = 2, 
        User = 3
    }

    public enum ArticleType
    {
        News = 1,
        Career = 2
    }

    public enum ConversationStatus
    {
        None = 1,
        Awaiting = 2, 
        Connected = 3, 
        Declined = 4
    }
}