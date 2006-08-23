//
// System.Drawing.TextureBrush unit tests
//
// Authors:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// Copyright (C) 2006 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Permissions;
using NUnit.Framework;

namespace MonoTests.System.Drawing {

	[TestFixture]
	[SecurityPermission (SecurityAction.Deny, UnmanagedCode = true)]
	public class TextureBrushTest {

		private Image image;
		private Rectangle rect;
		private RectangleF rectf;
		private ImageAttributes attr;

		[TestFixtureSetUp]
		public void FixtureSetUp ()
		{
			image = new Bitmap (10, 10);
			rect = new Rectangle (0, 0, 10, 10);
			rectf = new RectangleF (0, 0, 10, 10);
			attr = new ImageAttributes ();
		}

		private void Common (TextureBrush t, WrapMode wm)
		{
			Assert.IsNotNull (t.Image, "Image");
			Assert.IsFalse (Object.ReferenceEquals (image, t.Image), "Image-Equals");
			Assert.IsTrue (t.Transform.IsIdentity, "Transform.IsIdentity");
			Assert.AreEqual (wm, t.WrapMode, "WrapMode");
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorImage_Null ()
		{
			new TextureBrush (null);
		}

		[Test]
		public void CtorImage ()
		{
			TextureBrush t = new TextureBrush (image);
			Common (t, WrapMode.Tile);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorImage_Null_WrapMode ()
		{
			new TextureBrush (null, WrapMode.Clamp);
		}

		[Test]
		public void CtorImageWrapMode ()
		{
			foreach (WrapMode wm in Enum.GetValues (typeof (WrapMode))) {
				TextureBrush t = new TextureBrush (image, wm);
				Common (t, wm);
			}
		}

		[Test]
		[ExpectedException (typeof (InvalidEnumArgumentException))]
		public void CtorImageWrapMode_Invalid ()
		{
			new TextureBrush (image, (WrapMode) Int32.MinValue);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorImage_Null_Rectangle ()
		{
			new TextureBrush (null, rect);
		}

		[Test]
		[ExpectedException (typeof (OutOfMemoryException))]
		public void CtorImageRectangle_Empty ()
		{
			new TextureBrush (image, new Rectangle ());
		}

		[Test]
		public void CtorImageRectangle ()
		{
			TextureBrush t = new TextureBrush (image, rect);
			Common (t, WrapMode.Tile);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorImage_Null_RectangleF ()
		{
			new TextureBrush (null, rectf);
		}

		[Test]
		[ExpectedException (typeof (OutOfMemoryException))]
		public void CtorImageRectangleF_Empty ()
		{
			new TextureBrush (image, new RectangleF ());
		}

		[Test]
		public void CtorImageRectangleF ()
		{
			TextureBrush t = new TextureBrush (image, rectf);
			Common (t, WrapMode.Tile);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorImage_Null_RectangleAttributes ()
		{
			new TextureBrush (null, rect, attr);
		}

		[Test]
		[ExpectedException (typeof (OutOfMemoryException))]
		public void CtorImageRectangle_Empty_Attributes ()
		{
			new TextureBrush (image, new Rectangle (), attr);
		}

		[Test]
		public void CtorImageRectangleAttributes_Null ()
		{
			TextureBrush t = new TextureBrush (image, rect, null);
			Common (t, WrapMode.Tile);
		}

		[Test]
		public void CtorImageRectangleAttributes ()
		{
			TextureBrush t = new TextureBrush (image, rect, attr);
			Common (t, WrapMode.Clamp);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorImage_Null_RectangleFAttributes ()
		{
			new TextureBrush (null, rectf, attr);
		}

		[Test]
		[ExpectedException (typeof (OutOfMemoryException))]
		public void CtorImageRectangleF_Empty_Attributes ()
		{
			new TextureBrush (image, new RectangleF ());
		}

		[Test]
		public void CtorImageRectangleFAttributes_Null ()
		{
			TextureBrush t = new TextureBrush (image, rectf, null);
			Common (t, WrapMode.Tile);
		}

		[Test]
		public void CtorImageRectangleFAttributes ()
		{
			TextureBrush t = new TextureBrush (image, rectf, attr);
			Common (t, WrapMode.Clamp);
		}

		[Test]
		public void CtorImageWrapModeRectangle ()
		{
			foreach (WrapMode wm in Enum.GetValues (typeof (WrapMode))) {
				TextureBrush t = new TextureBrush (image, wm, rect);
				Common (t, wm);
			}
		}

		[Test]
		[ExpectedException (typeof (InvalidEnumArgumentException))]
		public void CtorImageWrapMode_Invalid_Rectangle ()
		{
			new TextureBrush (image, (WrapMode) Int32.MinValue, rect);
		}

		[Test]
		public void CtorImageWrapModeRectangleF ()
		{
			foreach (WrapMode wm in Enum.GetValues (typeof (WrapMode))) {
				TextureBrush t = new TextureBrush (image, wm, rectf);
				Common (t, wm);
			}
		}

		[Test]
		[ExpectedException (typeof (InvalidEnumArgumentException))]
		public void CtorImageWrapMode_Invalid_RectangleF ()
		{
			new TextureBrush (image, (WrapMode) Int32.MinValue, rectf);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Transform_Null ()
		{
			new TextureBrush (image).Transform = null;
		}

		[Test]
		public void Transform ()
		{
			Matrix m = new Matrix ();
			TextureBrush t = new TextureBrush (image);
			t.Transform = m;
			Assert.IsFalse (Object.ReferenceEquals (m, t.Transform));
		}

		[Test]
		public void WrapMode_Valid ()
		{
			foreach (WrapMode wm in Enum.GetValues (typeof (WrapMode))) {
				TextureBrush t = new TextureBrush (image);
				t.WrapMode = wm;
				Assert.AreEqual (wm, t.WrapMode, wm.ToString ());
			}
		}

		[Test]
		[ExpectedException (typeof (InvalidEnumArgumentException))]
		public void WrapMode_Invalid ()
		{
			new TextureBrush (image).WrapMode = (WrapMode)Int32.MinValue;
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void Dispose_Clone ()
		{
			TextureBrush t = new TextureBrush (image);
			t.Dispose ();
			t.Clone ();
		}

		[Test]
		public void Dispose_Dispose ()
		{
			TextureBrush t = new TextureBrush (image);
			t.Dispose ();
			t.Dispose ();
		}

		[Test]
		[NUnit.Framework.Category ("NotDotNet")] // AccessViolationException under 2.0
		[ExpectedException (typeof (ArgumentException))]
		public void Dispose_Image ()
		{
			TextureBrush t = new TextureBrush (image);
			t.Dispose ();
			Assert.IsNotNull (t.Image, "Image");
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void MultiplyTransform_Null ()
		{
			new TextureBrush (image).MultiplyTransform (null);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void MultiplyTransform_Null_Order ()
		{
			new TextureBrush (image).MultiplyTransform (null, MatrixOrder.Append);
		}

		[Test]
		public void MultiplyTransformOrder_Invalid ()
		{
			TextureBrush t = new TextureBrush (image);
			t.MultiplyTransform (new Matrix (), (MatrixOrder) Int32.MinValue);
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void MultiplyTransform_NonInvertible ()
		{
			TextureBrush t = new TextureBrush (image);
			Matrix noninvertible = new Matrix (123, 24, 82, 16, 47, 30);
			t.MultiplyTransform (noninvertible);
		}

		[Test]
		public void ResetTransform ()
		{
			TextureBrush t = new TextureBrush (image);
			t.RotateTransform (90);
			Assert.IsFalse (t.Transform.IsIdentity, "Transform.IsIdentity");
			t.ResetTransform ();
			Assert.IsTrue (t.Transform.IsIdentity, "Reset.IsIdentity");
		}

		[Test]
		[NUnit.Framework.Category ("NotWorking")]
		public void RotateTransform ()
		{
			TextureBrush t = new TextureBrush (image);
			t.RotateTransform (90);
			float[] elements = t.Transform.Elements;
			Assert.AreEqual (0, elements[0], 0.1, "matrix.0");
			Assert.AreEqual (1, elements[1], 0.1, "matrix.1");
			Assert.AreEqual (-1, elements[2], 0.1, "matrix.2");
			Assert.AreEqual (0, elements[3], 0.1, "matrix.3");
			Assert.AreEqual (0, elements[4], 0.1, "matrix.4");
			Assert.AreEqual (0, elements[5], 0.1, "matrix.5");

			t.RotateTransform (270);
			Assert.IsTrue (t.Transform.IsIdentity, "Transform.IsIdentity");
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void RotateTransform_InvalidOrder ()
		{
			TextureBrush t = new TextureBrush (image);
			t.RotateTransform (720, (MatrixOrder) Int32.MinValue);
		}

		[Test]
		public void ScaleTransform ()
		{
			TextureBrush t = new TextureBrush (image);
			t.ScaleTransform (2, 4);
			float[] elements = t.Transform.Elements;
			Assert.AreEqual (2, elements[0], 0.1, "matrix.0");
			Assert.AreEqual (0, elements[1], 0.1, "matrix.1");
			Assert.AreEqual (0, elements[2], 0.1, "matrix.2");
			Assert.AreEqual (4, elements[3], 0.1, "matrix.3");
			Assert.AreEqual (0, elements[4], 0.1, "matrix.4");
			Assert.AreEqual (0, elements[5], 0.1, "matrix.5");

			t.ScaleTransform (0.5f, 0.25f);
			Assert.IsTrue (t.Transform.IsIdentity, "Transform.IsIdentity");
		}

		[Test]
		public void ScaleTransform_MaxMin ()
		{
			TextureBrush t = new TextureBrush (image);
			t.ScaleTransform (Single.MaxValue, Single.MinValue);
			float[] elements = t.Transform.Elements;
			Assert.AreEqual (Single.MaxValue, elements[0], 1e33, "matrix.0");
			Assert.AreEqual (0, elements[1], 0.1, "matrix.1");
			Assert.AreEqual (0, elements[2], 0.1, "matrix.2");
			Assert.AreEqual (Single.MinValue, elements[3], 1e33, "matrix.3");
			Assert.AreEqual (0, elements[4], 0.1, "matrix.4");
			Assert.AreEqual (0, elements[5], 0.1, "matrix.5");
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void ScaleTransform_InvalidOrder ()
		{
			TextureBrush t = new TextureBrush (image);
			t.ScaleTransform (1, 1, (MatrixOrder) Int32.MinValue);
		}

		[Test]
		public void TranslateTransform ()
		{
			TextureBrush t = new TextureBrush (image);
			t.TranslateTransform (1, 1);
			float[] elements = t.Transform.Elements;
			Assert.AreEqual (1, elements[0], 0.1, "matrix.0");
			Assert.AreEqual (0, elements[1], 0.1, "matrix.1");
			Assert.AreEqual (0, elements[2], 0.1, "matrix.2");
			Assert.AreEqual (1, elements[3], 0.1, "matrix.3");
			Assert.AreEqual (1, elements[4], 0.1, "matrix.4");
			Assert.AreEqual (1, elements[5], 0.1, "matrix.5");

			t.TranslateTransform (-1, -1);
			elements = t.Transform.Elements;
			Assert.AreEqual (1, elements[0], 0.1, "revert.matrix.0");
			Assert.AreEqual (0, elements[1], 0.1, "revert.matrix.1");
			Assert.AreEqual (0, elements[2], 0.1, "revert.matrix.2");
			Assert.AreEqual (1, elements[3], 0.1, "revert.matrix.3");
			Assert.AreEqual (0, elements[4], 0.1, "revert.matrix.4");
			Assert.AreEqual (0, elements[5], 0.1, "revert.matrix.5");
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void TranslateTransform_InvalidOrder ()
		{
			TextureBrush t = new TextureBrush (image);
			t.TranslateTransform (1, 1, (MatrixOrder) Int32.MinValue);
		}
	}
}
