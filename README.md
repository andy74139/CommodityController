GroupSums
====

GroupSums supplies get group sums of specified data.

## Framework
* Visual Studio 2013
* .NET Framework 4.5
* C#

## Usage
<pre>
//_Products = new List&lt;Product&gt;{new Product(...), ...};
GroupSums groupSums = new GroupSums(_Products);
IEnumerable&lt;int&gt; sums = groupSums.Get(amountPerGroup, dataName);
</pre>
Get group sums of data by passing products to the object when constructing it first. Then use Get() to get them.

## Examples
<pre>
var _TestProducts = new List&lt;Product&gt;()
{
    new Product(id:1, cost:1, revenue:11, sellPrice:21),
    new Product(2, 2, 12, 22),
    new Product(3, 3, 13, 23),
    new Product(4, 4, 14, 24),
    new Product(5, 5, 15, 25),
    new Product(6, 6, 16, 26),
    new Product(7, 7, 17, 27),
    new Product(8, 8, 18, 28),
    new Product(9, 9, 19, 29),
    new Product(10, 10, 20, 30),
    new Product(11, 11, 21, 31), 
};

 var target = new GroupSums(_TestProducts);
 var sums1 = target.Get(1, "Cost"   ); // IEnumerable&lt;int&gt; { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }
 var sums2 = target.Get(11, "Cost"   ); // IEnumerable&lt;int&gt; { 66 }
 var sums3 = target.Get(3, "Cost"   ); // IEnumerable&lt;int&gt; { 6, 15, 24, 21 }
 var sums4 = target.Get(4, "Revenue"); // IEnumerable&lt;int&gt; { 50, 66, 60 }
 </pre>

## Constructor
> GroupSums(IEnumerable&lt;Product&gt;)

Construct GroupsSums by passing the products.

## Method
> IEnumerable&lt;int&gt; Get(int, string)

Get groups sums according to amount per group and specified data name to sum.

It throws ArgumentException if amountPerGroup is not positive or cannot find specified data name.
