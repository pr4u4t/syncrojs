namespace Microsoft.Tools.Graphs.Pies
{
	using System;
	using System.Collections;

	//  Project   : Microsoft.Tools.Graphs.Pies
	//  Class     : PieSliceCollection
	// 
	//  Copyright (C) 2002, Microsoft Corporation
	// ------------------------------------------------------------------------------
	//  <summary>
	//  Strongly-typed collection of PieSlice objects
	//  </summary>
	//  <remarks></remarks>
	//  <history>
	//      [dineshc] 10/4/2003  Created
	//  </history>
	[Serializable()]
	public class PieSliceCollection: CollectionBase
	{
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/>.
		///  </summary>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public PieSliceCollection()
		{
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> based on another <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/>.
		///  </summary>
		///  <param name="value">
		///       A <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> from which the contents are copied
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public PieSliceCollection(PieSliceCollection value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> containing any array of <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> objects.
		///  </summary>
		///  <param name="value">
		///       A array of <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> objects with which to intialize the collection
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public PieSliceCollection(PieSlice[] value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///  Represents the entry at the specified index of the <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/>.
		///  </summary>
		///  <param name="index">The zero-based index of the entry to locate in the collection.</param>
		///  <value>
		///  The entry at the specified index of the collection.
		///  </value>
		///  <remarks><exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the valid range of indexes for the collection.</exception></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public PieSlice this[int index]
		{
			get
			{
				return ((PieSlice)(List[index]));
			}
			set
			{
				List[index] = value;
			}
		}
		///  <summary>
		///    Adds a <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> with the specified value to the 
		///    <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> to add.</param>
		///  <returns>
		///    The index at which the new element was inserted.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection.AddRange"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public int Add(PieSlice value)
		{
			return List.Add(value);
		}
		///  <summary>
		///  Copies the elements of an array to the end of the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/>.
		///  </summary>
		///  <param name="value">
		///    An array of type <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public void AddRange(PieSlice[] value)
		{
			for (int i = 0; (i < value.Length); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///     
		///       Adds the contents of another <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> to the end of the collection.
		///    
		///  </summary>
		///  <param name="value">
		///    A <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public void AddRange(PieSliceCollection value)
		{
			for (int i = 0; (i < value.Count); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///  Gets a value indicating whether the 
		///    <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> contains the specified <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/>.
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> to locate.</param>
		///  <returns>
		///  <see langword="true"/> if the <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> is contained in the collection; 
		///   otherwise, <see langword="false"/>.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection.IndexOf"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public bool Contains(PieSlice value)
		{
			return List.Contains(value);
		}
		///  <summary>
		///  Copies the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> values to a one-dimensional <see cref="System.Array"/> instance at the 
		///    specified index.
		///  </summary>
		///  <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the values copied from <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> .</param>
		///  <param name="index">The index in <paramref name="array"/> where copying begins.</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="array"/> is multidimensional. <para>-or-</para> <para>The number of elements in the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> is greater than the available space between <paramref name="arrayIndex"/> and the end of <paramref name="array"/>.</para></exception>
		///  <exception cref="System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>. </exception>
		///  <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than <paramref name="array"/>"s lowbound. </exception>
		///  <seealso cref="System.Array"/>
		///  </remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public void CopyTo(PieSlice[] array, int index)
		{
			List.CopyTo(array, index);
		}
		///  <summary>
		///    Returns the index of a <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> in 
		///       the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> to locate.</param>
		///  <returns>
		///  The index of the <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> of <paramref name="value"/> in the 
		///  <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/>, if found; otherwise, -1.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection.Contains"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public int IndexOf(PieSlice value)
		{
			return List.IndexOf(value);
		}
		///  <summary>
		///  Inserts a <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> into the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> at the specified index.
		///  </summary>
		///  <param name="index">The zero-based index where <paramref name="value"/> should be inserted.</param>
		///  <param name=" value">The <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> to insert.</param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public void Insert(int index, PieSlice value)
		{
			List.Insert(index, value);
		}
		///  <summary>
		///    Returns an enumerator that can iterate through 
		///       the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> .
		///  </summary>
		///  <returns>An enumerator for the collection</returns>
		///  <remarks><seealso cref="System.Collections.IEnumerator"/></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public new PieSliceEnumerator GetEnumerator()
		{
			return new PieSliceEnumerator(this);
		}
		///  <summary>
		///     Removes a specific <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> from the 
		///    <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Pies.PieSlice"/> to remove from the <see cref="Microsoft.Tools.Graphs.Pies.PieSliceCollection"/> .</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="value"/> is not found in the Collection. </exception></remarks>
		///  <history>
		///      [dineshc] 10/4/2003  Created
		///  </history>
		public void Remove(PieSlice value)
		{
			List.Remove(value);
		}
		public class PieSliceEnumerator: object, IEnumerator
		{
			private IEnumerator baseEnumerator;
			private IEnumerable temp;
			public PieSliceEnumerator(PieSliceCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			public PieSlice Current
			{
				get
				{
					return ((PieSlice)(baseEnumerator.Current));
				}
			}
			object IEnumerator.Current
			{
				get
				{
					return baseEnumerator.Current;
				}
			}
			public bool MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			bool IEnumerator.MoveNext()
			{
				return baseEnumerator.MoveNext();
			}
			public void Reset()
			{
				baseEnumerator.Reset();
			}
			void IEnumerator.Reset()
			{
				baseEnumerator.Reset();
			}
		}
	}
}
