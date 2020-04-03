
// 统一的增删改系统接口。
// 很多系统都需要客户端缓存状态，对于增删改查系统，这些接口用来规范统一的行为
// 实际上并不会带来任何效率提升，只是为了规范统一

using System.Collections.Generic;

namespace HG
{
    /// <summary>
    /// 针对数据的增删改查系统
    /// </summary>
    public interface ICRUDSystem<T>
    {
        /// <summary>
        /// 初始化所有数据
        /// </summary>
        /// <param name="allData">应该是个数据集合，比如dic,list,array</param>
        void InitAll(ICollection<T> allData);

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="toAdd">待添加元素</param>
        /// <returns></returns>
        T Create(T toAdd);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="toDelete">待删除元素</param>
        /// <returns>删除结果</returns>
        bool Delete(T toDelete);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="toUpdate">待更新数据</param>
        /// <returns>更新后的结果</returns>
        T Update(T toUpdate);

        /// <summary>
        /// 清除所有数据
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// 带有视图表现的增删改系统
    /// </summary>
    public interface ICRUDSystemWithView<T>: ICRUDSystem<T> where T :ICRUDViewUnit
    {
        /// <summary>
        /// 在视图上显示这个对象
        /// </summary>
        /// <param name="target">目标对象</param>
        void Show(T target);

        /// <summary>
        /// 显示所有对象
        /// </summary>
        void ShowAll();

        /// <summary>
        /// 回收所有对象
        /// </summary>
        void ReleaseAll();

        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="clearData">是否把数据也清理掉</param>
        /// 有些情况在清理时只清理表现而不清理数据。比如slg内城外城场景切换，之清理对象而保存数据
        void Clear(bool clearData);
    }

    /// <summary>
    /// 增删改数据单元。
    /// 实际上不应该定义这个接口，因为数据类无法让外部的类继承该接口，比如protobuf自动生成的类
    /// 而重新定义一个类去继承这个接口也没必要，反而加大了内存开销--多了没必要的对象复制。
    /// </summary>
    public interface ICRUDUnit
    {
        
    }

    /// <summary>
    /// 带有视图显示的单元对象
    /// </summary>
    public interface ICRUDViewUnit
    {
        /// <summary>
        /// 显示该对象
        /// </summary>
        void Show();
        
        /// <summary>
        /// 隐藏该对象
        /// </summary>
        void Hide();
        
        /// <summary>
        /// 回收该对象
        /// </summary>
        void Release();
    }
}