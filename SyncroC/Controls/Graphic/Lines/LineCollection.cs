namespace Microsoft.Tools.Graphs.Lines
{
	using System;
	using System.Collections;

	//  Project   : Microsoft.Tools.Graphs.Lines
	//  Class     : LineCollection
	// 
	//  Copyright (C) 2002, Microsoft Corporation
	// ------------------------------------------------------------------------------
	//  <summary>
	//  Strongly-typed collection of Line objects
	//  </summary>
	//  <remarks></remarks>
	//  <history>
	//      [dineshc] 9/24/2003  Created
	//  </history>
	[Serializable()]
	public class LineCollection: CollectionBase
	{
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/>.
		///  </summary>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LineCollection()
		{
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> based on another <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/>.
		///  </summary>
		///  <param name="value">
		///       A <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> from which the contents are copied
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LineCollection(LineCollection value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///       Initializes a new instance of <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> containing any array of <see cref="Microsoft.Tools.Graphs.Lines.Line"/> objects.
		///  </summary>
		///  <param name="value">
		///       A array of <see cref="Microsoft.Tools.Graphs.Lines.Line"/> objects with which to intialize the collection
		///  </param>
		///  <remarks></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public LineCollection(Line[] value)
		{
			this.AddRange(value);
		}
		///  <summary>
		///  Represents the entry at the specified index of the <see cref="Microsoft.Tools.Graphs.Lines.Line"/>.
		///  </summary>
		///  <param name="index">The zero-based index of the entry to locate in the collection.</param>
		///  <value>
		///  The entry at the specified index of the collection.
		///  </value>
		///  <remarks><exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the valid range of indexes for the collection.</exception></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public Line this[int index]
		{
			get
			{
				return ((Line)(List[index]));
			}
			set
			{
				List[index] = value;
			}
		}
		///  <summary>
		///    Adds a <see cref="Microsoft.Tools.Graphs.Lines.Line"/> with the specified value to the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.Line"/> to add.</param>
		///  <returns>
		///    The index at which the new element was inserted.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LineCollection.AddRange"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public int Add(Line value)
		{
			return List.Add(value);
		}
		///  <summary>
		///  Copies the elements of an array to the end of the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/>.
		///  </summary>
		///  <param name="value">
		///    An array of type <see cref="Microsoft.Tools.Graphs.Lines.Line"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LineCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void AddRange(Line[] value)
		{
			for (int i = 0; (i < value.Length); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///     
		///       Adds the contents of another <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> to the end of the collection.
		///    
		///  </summary>
		///  <param name="value">
		///    A <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> containing the objects to add to the collection.
		///  </param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LineCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void AddRange(LineCollection value)
		{
			for (int i = 0; (i < value.Count); i = (i + 1))
			{
				this.Add(value[i]);
			}
		}
		///  <summary>
		///  Gets a value indicating whether the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> contains the specified <see cref="Microsoft.Tools.Graphs.Lines.Line"/>.
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.Line"/> to locate.</param>
		///  <returns>
		///  <see langword="true"/> if the <see cref="Microsoft.Tools.Graphs.Lines.Line"/> is contained in the collection; 
		///   otherwise, <see langword="false"/>.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LineCollection.IndexOf"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public bool Contains(Line value)
		{
			return List.Contains(value);
		}
		///  <summary>
		///  Copies the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> values to a one-dimensional <see cref="System.Array"/> instance at the 
		///    specified index.
		///  </summary>
		///  <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the values copied from <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> .</param>
		///  <param name="index">The index in <paramref name="array"/> where copying begins.</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="array"/> is multidimensional. <para>-or-</para> <para>The number of elements in the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> is greater than the available space between <paramref name="arrayIndex"/> and the end of <paramref name="array"/>.</para></exception>
		///  <exception cref="System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>. </exception>
		///  <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than <paramref name="array"/>"s lowbound. </exception>
		///  <seealso cref="System.Array"/>
		///  </remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void CopyTo(Line[] array, int index)
		{
			List.CopyTo(array, index);
		}
		///  <summary>
		///    Returns the index of a <see cref="Microsoft.Tools.Graphs.Lines.Line"/> in 
		///       the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.Line"/> to locate.</param>
		///  <returns>
		///  The index of the <see cref="Microsoft.Tools.Graphs.Lines.Line"/> of <paramref name="value"/> in the 
		///  <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/>, if found; otherwise, -1.
		///  </returns>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LineCollection.Contains"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public int IndexOf(Line value)
		{
			return List.IndexOf(value);
		}
		///  <summary>
		///  Inserts a <see cref="Microsoft.Tools.Graphs.Lines.Line"/> into the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> at the specified index.
		///  </summary>
		///  <param name="index">The zero-based index where <paramref name="value"/> should be inserted.</param>
		///  <param name=" value">The <see cref="Microsoft.Tools.Graphs.Lines.Line"/> to insert.</param>
		///  <remarks><seealso cref="Microsoft.Tools.Graphs.Lines.LineCollection.Add"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void Insert(int index, Line value)
		{
			List.Insert(index, value);
		}
		///  <summary>
		///    Returns an enumerator that can iterate through 
		///       the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> .
		///  </summary>
		///  <returns>An enumerator for the collection</returns>
		///  <remarks><seealso cref="System.Collections.IEnumerator"/></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public new LineEnumerator GetEnumerator()
		{
			return new LineEnumerator(this);
		}
		///  <summary>
		///     Removes a specific <see cref="Microsoft.Tools.Graphs.Lines.Line"/> from the 
		///    <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> .
		///  </summary>
		///  <param name="value">The <see cref="Microsoft.Tools.Graphs.Lines.Line"/> to remove from the <see cref="Microsoft.Tools.Graphs.Lines.LineCollection"/> .</param>
		///  <remarks><exception cref="System.ArgumentException"><paramref name="value"/> is not found in the Collection. </exception></remarks>
		///  <history>
		///      [dineshc] 9/24/2003  Created
		///  </history>
		public void Remove(Line value)
		{
			List.Remove(value);
		}
		public class LineEnumerator: object, IEnumerator
		{
			private IEnumerator baseEnumerator;
			private IEnumerable temp;
			public LineEnumerator(LineCollection mappings)
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			public Line Current
			{
				get
				{
					return ((Line)(baseEnumerator.Current));
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
