//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.10.102
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace Umbraco.Web.PublishedContentModels
{
	// Mixin content Type 1058 with alias "mastheadImage"
	/// <summary>Masthead Image</summary>
	public partial interface IMastheadImage : IPublishedContent
	{
		/// <summary>Masthead</summary>
		Umbraco.Web.Models.ImageCropDataSet Masthead { get; }
	}

	/// <summary>Masthead Image</summary>
	[PublishedContentModel("mastheadImage")]
	public partial class MastheadImage : PublishedContentModel, IMastheadImage
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "mastheadImage";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public MastheadImage(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<MastheadImage, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Masthead
		///</summary>
		[ImplementPropertyType("masthead")]
		public Umbraco.Web.Models.ImageCropDataSet Masthead
		{
			get { return GetMasthead(this); }
		}

		/// <summary>Static getter for Masthead</summary>
		public static Umbraco.Web.Models.ImageCropDataSet GetMasthead(IMastheadImage that) { return that.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("masthead"); }
	}
}