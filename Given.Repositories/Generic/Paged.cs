using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Given.Repositories.Generic
{
    public interface IPaged<T> : IEnumerable<T>
    {
        ///<summary>
        /// Get the total entity count.
        ///</summary>
        int Count { get; }

        ///<summary>
        /// Get a range of persited entities.
        ///</summary>
        IEnumerable<T> GetRange(int index, int count);
    }


    public class Paged<T> : IPaged<T>
    {
        private readonly IQueryable<T> source;

        public Paged(IQueryable<T> source)
        {
            this.source = source;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }     

        public int Count
        {
            get { return source.Count(); }
        }

        public IEnumerable<T> GetRange(int index, int count)
        {
            return source.Skip(index).Take(count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
