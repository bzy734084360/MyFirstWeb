using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepository.IRepository
{
    /// <summary>
    /// 博客-授权客户端仓储I
    /// </summary>
    public interface IBlogClientRepository<T> : IRepositoryBase<T>
    {
        //新增对应业务的接口
    }
}
