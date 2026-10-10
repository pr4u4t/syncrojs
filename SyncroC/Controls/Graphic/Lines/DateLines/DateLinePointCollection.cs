namespace Microsoft.Tools.Graphs.Lines.DateLines
{
	using System;
	using System.Collections;

	//  Project   : Microsoft.Tools.Graphs.Lines
	//  Class     : DateLinePointCollection
	// 
	//  Copyright (C) 2002, Microsoft Corporation
	// ------------------------------------------------------------------------------
	//  <summary>
	//  Strongly-typed collection of DateLinePoint objects
	//  </summary>
	//  <remarks></remarks>
	//  <history>
	//      [dineshc] 10/1/2003  Created
	//  </history>
	[Serializable()]
	public class DateLinePointCollection: CollectionBase
	{
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/>.
		///  </summary>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public DateLinePointCollection()
		{
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> based on another <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/>.
		///  </summary>
		///  <param name="value">
		///       A <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> from which the contents are copied
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public DateLinePointCollection(DateLinePointCollection value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> containing any array of <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> objects.
		///  </summary>
		///  <param name="value">
		///       A array of <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> objects with which to intialize the collection
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public DateLinePointCollection(DateLinePoint[] value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///  Represents the entry at the specified index of the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/>.
		///  </summary>
		///  <param name="index">The zero-based index of the entry to locate in the collection.</param>
		///  <value>
		///  The entry at the specified index of the collection.
		///  </value>
		///  <remarks><exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the valid range of indexes for the collection.</exception></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public DateLinePoint this[int index]
		{
			get
			{
				return ((DateLinePoint)(List[index]));
			}
			set
			{
				List[index] = value;
			}
		}
		///  <summary>
		///    Adds a <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> with the specified value to the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> to add.</param>
		///  <returns>
		///    The index at which the new element was inserted.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection.AddRange"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public int Add(DateLinePoint value)
		{
			return List.Add(value);
		}
		///  <summary>
		///  Copies the elements of an array to the end of the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/>.
		///  </summary>
		///  <param name="value">
		///    An array of type <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public void AddRange(DateLinePoint[] value)
		{
			for (int i = 0; (i < value.Length); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///     
		///       Adds the contents of another <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> to the end of the collection.
		///    
		///  </summary>
		///  <param name="value">
		///    A <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public void AddRange(DateLinePointCollection value)
		{
			for (int i = 0; (i < value.Count); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///  Gets a value indicating whether the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> contains the specified <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/>.
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> to locate.</param>
		///  <returns>
		///  <see langword="true"/> if the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> is contained in the collection; 
		///   otherwise, <see langword="false"/>.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection.IndexOf"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public bool Contains(DateLinePoint value)
		{
			return List.Contains(value);
		}
		///  <summary>
		///  Copies the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> values to a one-dimensional <see cref="System.Array"/> instance at the 
		///    specified index.
		///  </summary>
		///  <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the values copied from <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> .</param>
		///  <param name="index">The index in <paramref name="array"/> where copying begins.</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="array"/> is multidimensional. <para>-or-</para> <para>The number of elements in the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> is greater than the available space between <paramref name="arrayIndex"/> and the end of <paramref name="array"/>.</para></exception>
		///  <exception cref="System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>. </exception>
		///  <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than <paramref name="array"/>"s lowbound. </exception>
		///  <seealso cref="System.Array"/>
		///  </remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public void CopyTo(DateLinePoint[] array, int index)
		{
			List.CopyTo(array, index);
		}
		///  <summary>
		///    Returns the index of a <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> in 
		///       the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> to locate.</param>
		///  <returns>
		///  The index of the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> of <paramref name="value"/> in the 
		///  <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/>, if found; otherwise, -1.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection.Contains"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public int IndexOf(DateLinePoint value)
		{
			return List.IndexOf(value);
		}
		///  <summary>
		///  Inserts a <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> into the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> at the specified index.
		///  </summary>
		///  <param name="index">The zero-based index where <paramref name="value"/> should be inserted.</param>
		///  <param name=" value">The <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> to insert.</param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public void Insert(int index, DateLinePoint value)
		{
			List.Insert(index, value);
		}
		///  <summary>
		///    Returns an enumerator that can iterate through 
		///       the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> .
		///  </summary>
		///  <returns>An enumerator for the collection</returns>
		///  <remarks><seealso cref="System.Collections.IEnumerator"/></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public new DateLinePointEnumerator GetEnumerator()
		{
			return new DateLinePointEnumerator(this);
		}
		///  <summary>
		///     Removes a specific <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> from the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.DateLinePoint"/> to remove from the <see cref="Microsoft.Tools.Graphs.Lines.DateLinePointCollection"/> .</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="value"/> is not found in the Collection. </exception></remarks>
		///  <history>
		///      [dineshc] 10/1/2003  Created
		///  </history>
		public void Remove(DateLinePoint value)
		{
			List.Remove(value);
		}
		public class DateLinePointEnumerator: object, IEnumerator
		{
			private IEnumerator baseEnumerator;
			private IEnumerable temp;
			public DateLinePointEnumerator(DateLinePointCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			public DateLinePoint Current
			{
				get
				{
					return ((DateLinePoint)(baseEnumerator.Current));
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
