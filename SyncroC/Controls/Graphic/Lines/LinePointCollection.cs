namespace Microsoft.Tools.Graphs.Lines
{
	using System;
	using System.Collections;

	//  Project   : Microsoft.Tools.Graphs.Lines
	//  Class     : LinePointCollection
	// 
	//  Copyright (C) 2002, Microsoft Corporation
	// ------------------------------------------------------------------------------
	//  <summary>
	//  Strongly-typed collection of LinePoint objects
	//  </summary>
	//  <remarks></remarks>
	//  <history>
	//      [dineshc] 9/24/2003  Created
	//  </history>
	[Serializable()]
	public class LinePointCollection: CollectionBase
	{
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/>.
		///  </summary>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LinePointCollection()
		{
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> based on another <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/>.
		///  </summary>
		///  <param name="value">
		///       A <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> from which the contents are copied
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LinePointCollection(LinePointCollection value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> containing any array of <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> objects.
		///  </summary>
		///  <param name="value">
		///       A array of <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> objects with which to intialize the collection
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LinePointCollection(LinePoint[] value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///  Represents the entry at the specified index of the <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/>.
		///  </summary>
		///  <param name="index">The zero-based index of the entry to locate in the collection.</param>
		///  <value>
		///  The entry at the specified index of the collection.
		///  </value>
		///  <remarks><exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the valid range of indexes for the collection.</exception></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LinePoint this[int index]
		{
			get
			{
				return ((LinePoint)(List[index]));
			}
			set
			{
				List[index] = value;
			}
		}
		///  <summary>
		///    Adds a <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> with the specified value to the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> to add.</param>
		///  <returns>
		///    The index at which the new element was inserted.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LinePointCollection.AddRange"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public int Add(LinePoint value)
		{
			return List.Add(value);
		}
		///  <summary>
		///  Copies the elements of an array to the end of the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/>.
		///  </summary>
		///  <param name="value">
		///    An array of type <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LinePointCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void AddRange(LinePoint[] value)
		{
			for (int i = 0; (i < value.Length); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///     
		///       Adds the contents of another <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> to the end of the collection.
		///    
		///  </summary>
		///  <param name="value">
		///    A <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LinePointCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void AddRange(LinePointCollection value)
		{
			for (int i = 0; (i < value.Count); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///  Gets a value indicating whether the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> contains the specified <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/>.
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> to locate.</param>
		///  <returns>
		///  <see langword="true"/> if the <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> is contained in the collection; 
		///   otherwise, <see langword="false"/>.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LinePointCollection.IndexOf"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public bool Contains(LinePoint value)
		{
			return List.Contains(value);
		}
		///  <summary>
		///  Copies the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> values to a one-dimensional <see cref="System.Array"/> instance at the 
		///    specified index.
		///  </summary>
		///  <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the values copied from <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> .</param>
		///  <param name="index">The index in <paramref name="array"/> where copying begins.</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="array"/> is multidimensional. <para>-or-</para> <para>The number of elements in the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> is greater than the available space between <paramref name="arrayIndex"/> and the end of <paramref name="array"/>.</para></exception>
		///  <exception cref="System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>. </exception>
		///  <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than <paramref name="array"/>"s lowbound. </exception>
		///  <seealso cref="System.Array"/>
		///  </remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void CopyTo(LinePoint[] array, int index)
		{
			List.CopyTo(array, index);
		}
		///  <summary>
		///    Returns the index of a <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> in 
		///       the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> to locate.</param>
		///  <returns>
		///  The index of the <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> of <paramref name="value"/> in the 
		///  <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/>, if found; otherwise, -1.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LinePointCollection.Contains"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public int IndexOf(LinePoint value)
		{
			return List.IndexOf(value);
		}
		///  <summary>
		///  Inserts a <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> into the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> at the specified index.
		///  </summary>
		///  <param name="index">The zero-based index where <paramref name="value"/> should be inserted.</param>
		///  <param name=" value">The <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> to insert.</param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LinePointCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void Insert(int index, LinePoint value)
		{
			List.Insert(index, value);
		}
		///  <summary>
		///    Returns an enumerator that can iterate through 
		///       the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> .
		///  </summary>
		///  <returns>An enumerator for the collection</returns>
		///  <remarks><seealso cref="System.Collections.IEnumerator"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public new LinePointEnumerator GetEnumerator()
		{
			return new LinePointEnumerator(this);
		}
		///  <summary>
		///     Removes a specific <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> from the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.LinePoint"/> to remove from the <see cref="Microsoft.Tools.Graphs.Lines.LinePointCollection"/> .</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="value"/> is not found in the Collection. </exception></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void Remove(LinePoint value)
		{
			List.Remove(value);
		}
		public class LinePointEnumerator: object, IEnumerator
		{
			private IEnumerator baseEnumerator;
			private IEnumerable temp;
			public LinePointEnumerator(LinePointCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			public LinePoint Current
			{
				get
				{
					return ((LinePoint)(baseEnumerator.Current));
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
