namespace Microsoft.Tools.Graphs.Legends {
    using System;
    using System.Collections;
    
    
    //  Project   : Microsoft.Tools.Graphs
    //  Class     : LegendEntryCollection
    // 
    //  Copyright (C) 2002, Microsoft Corporation
    // ------------------------------------------------------------------------------
    //  <summary>
    //  Strongly-typed collection of LegendEntry objects
    //  </summary>
    //  <remarks></remarks>
    //  <history>
    //      [dineshc] 7/15/2003  Created
    //  </history>
    [Serializable()]
    public class LegendEntryCollection : CollectionBase {
        
        ///  <summary>
        ///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/>.
        ///  </summary>
        ///  <remarks></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public LegendEntryCollection() {
        }
        
        ///  <summary>
        ///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> based on another <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/>.
        ///  </summary>
        ///  <param name="value">
        ///       A <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> from which the contents are copied
        ///  </param>
        ///  <remarks></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public LegendEntryCollection(LegendEntryCollection value) {
            this.AddRange(value);
        }
        
        ///  <summary>
        ///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> containing any array of <see cref="Microsoft.Tools.Graphs.LegendEntry"/> objects.
        ///  </summary>
        ///  <param name="value">
        ///       A array of <see cref="Microsoft.Tools.Graphs.LegendEntry"/> objects with which to intialize the collection
        ///  </param>
        ///  <remarks></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public LegendEntryCollection(LegendEntry[] value) {
            this.AddRange(value);
        }
        
        ///  <summary>
        ///  Represents the entry at the specified index of the <see cref="Microsoft.Tools.Graphs.LegendEntry"/>.
        ///  </summary>
        ///  <param name="index">The zero-based index of the entry to locate in the collection.</param>
        ///  <value>
        ///  The entry at the specified index of the collection.
        ///  </value>
        ///  <remarks><exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the valid range of indexes for the collection.</exception></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public LegendEntry this[int index] {
            get {
                return ((LegendEntry)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
        
        ///  <summary>
        ///    Adds a <see cref="Microsoft.Tools.Graphs.LegendEntry"/> with the specified value to the 
        ///    <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> .
        ///  </summary>
        ///  <param name="value">The <see cref="Microsoft.Tools.Graphs.LegendEntry"/> to add.</param>
        ///  <returns>
        ///    The index at which the new element was inserted.
        ///  </returns>
        ///  <remarks><seealso cref="Microsoft.Tools.Graphs.LegendEntryCollection.AddRange"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public int Add(LegendEntry value) {
            return List.Add(value);
        }
        
        ///  <summary>
        ///  Copies the elements of an array to the end of the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/>.
        ///  </summary>
        ///  <param name="value">
        ///    An array of type <see cref="Microsoft.Tools.Graphs.LegendEntry"/> containing the objects to add to the collection.
        ///  </param>
        ///  <remarks><seealso cref="Microsoft.Tools.Graphs.LegendEntryCollection.Add"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public void AddRange(LegendEntry[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        ///  <summary>
        ///     
        ///       Adds the contents of another <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> to the end of the collection.
        ///    
        ///  </summary>
        ///  <param name="value">
        ///    A <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> containing the objects to add to the collection.
        ///  </param>
        ///  <remarks><seealso cref="Microsoft.Tools.Graphs.LegendEntryCollection.Add"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public void AddRange(LegendEntryCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        ///  <summary>
        ///  Gets a value indicating whether the 
        ///    <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> contains the specified <see cref="Microsoft.Tools.Graphs.LegendEntry"/>.
        ///  </summary>
        ///  <param name="value">The <see cref="Microsoft.Tools.Graphs.LegendEntry"/> to locate.</param>
        ///  <returns>
        ///  <see langword="true"/> if the <see cref="Microsoft.Tools.Graphs.LegendEntry"/> is contained in the collection; 
        ///   otherwise, <see langword="false"/>.
        ///  </returns>
        ///  <remarks><seealso cref="Microsoft.Tools.Graphs.LegendEntryCollection.IndexOf"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public bool Contains(LegendEntry value) {
            return List.Contains(value);
        }
        
        ///  <summary>
        ///  Copies the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> values to a one-dimensional <see cref="System.Array"/> instance at the 
        ///    specified index.
        ///  </summary>
        ///  <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the values copied from <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> .</param>
        ///  <param name="index">The index in <paramref name="array"/> where copying begins.</param>
        ///  <remarks><exception cref="System.ArgumentException"><paramref name="array"/> is multidimensional. <para>-or-</para> <para>The number of elements in the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> is greater than the available space between <paramref name="arrayIndex"/> and the end of <paramref name="array"/>.</para></exception>
        ///  <exception cref="System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>. </exception>
        ///  <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than <paramref name="array"/>"s lowbound. </exception>
        ///  <seealso cref="System.Array"/>
        ///  </remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public void CopyTo(LegendEntry[] array, int index) {
            List.CopyTo(array, index);
        }
        
        ///  <summary>
        ///    Returns the index of a <see cref="Microsoft.Tools.Graphs.LegendEntry"/> in 
        ///       the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> .
        ///  </summary>
        ///  <param name="value">The <see cref="Microsoft.Tools.Graphs.LegendEntry"/> to locate.</param>
        ///  <returns>
        ///  The index of the <see cref="Microsoft.Tools.Graphs.LegendEntry"/> of <paramref name="value"/> in the 
        ///  <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/>, if found; otherwise, -1.
        ///  </returns>
        ///  <remarks><seealso cref="Microsoft.Tools.Graphs.LegendEntryCollection.Contains"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public int IndexOf(LegendEntry value) {
            return List.IndexOf(value);
        }
        
        ///  <summary>
        ///  Inserts a <see cref="Microsoft.Tools.Graphs.LegendEntry"/> into the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> at the specified index.
        ///  </summary>
        ///  <param name="index">The zero-based index where <paramref name="value"/> should be inserted.</param>
        ///  <param name=" value">The <see cref="Microsoft.Tools.Graphs.LegendEntry"/> to insert.</param>
        ///  <remarks><seealso cref="Microsoft.Tools.Graphs.LegendEntryCollection.Add"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public void Insert(int index, LegendEntry value) {
            List.Insert(index, value);
        }
        
        ///  <summary>
        ///    Returns an enumerator that can iterate through 
        ///       the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> .
        ///  </summary>
        ///  <returns>An enumerator for the collection</returns>
        ///  <remarks><seealso cref="System.Collections.IEnumerator"/></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public new LegendEntryEnumerator GetEnumerator() {
            return new LegendEntryEnumerator(this);
        }
        
        ///  <summary>
        ///     Removes a specific <see cref="Microsoft.Tools.Graphs.LegendEntry"/> from the 
        ///    <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> .
        ///  </summary>
        ///  <param name="value">The <see cref="Microsoft.Tools.Graphs.LegendEntry"/> to remove from the <see cref="Microsoft.Tools.Graphs.LegendEntryCollection"/> .</param>
        ///  <remarks><exception cref="System.ArgumentException"><paramref name="value"/> is not found in the Collection. </exception></remarks>
        ///  <history>
        ///      [dineshc] 7/15/2003  Created
        ///  </history>
        public void Remove(LegendEntry value) {
            List.Remove(value);
        }
        
        public class LegendEntryEnumerator : object, IEnumerator {
            
            private IEnumerator baseEnumerator;
            
            private IEnumerable temp;
            
            public LegendEntryEnumerator(LegendEntryCollection mappings) {
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }
            
            public LegendEntry Current {
                get {
                    return ((LegendEntry)(baseEnumerator.Current));
                }
            }
            
            object IEnumerator.Current {
                get {
                    return baseEnumerator.Current;
                }
            }
            
            public bool MoveNext() {
                return baseEnumerator.MoveNext();
            }
            
            bool IEnumerator.MoveNext() {
                return baseEnumerator.MoveNext();
            }
            
            public void Reset() {
                baseEnumerator.Reset();
            }
            
            void IEnumerator.Reset() {
                baseEnumerator.Reset();
            }
        }
    }
}
