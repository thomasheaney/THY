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
	/// <summary>Blog Post</summary>
	[PublishedContentModel("blogPost")]
	public partial class BlogPost : PublishedContentModel, IContent, IMastheadImage, ISummaryInfo, ITaxonomy
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "blogPost";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public BlogPost(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<BlogPost, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Grid Editor
		///</summary>
		[ImplementPropertyType("gridEditor")]
		public Newtonsoft.Json.Linq.JToken GridEditor
		{
			get { return Umbraco.Web.PublishedContentModels.Content.GetGridEditor(this); }
		}

		///<summary>
		/// Masthead
		///</summary>
		[ImplementPropertyType("masthead")]
		public Umbraco.Web.Models.ImageCropDataSet Masthead
		{
			get { return Umbraco.Web.PublishedContentModels.MastheadImage.GetMasthead(this); }
		}

		///<summary>
		/// Summary Image
		///</summary>
		[ImplementPropertyType("summaryImage")]
		public IPublishedContent SummaryImage
		{
			get { return Umbraco.Web.PublishedContentModels.SummaryInfo.GetSummaryImage(this); }
		}

		///<summary>
		/// Summary Text
		///</summary>
		[ImplementPropertyType("summaryText")]
		public string SummaryText
		{
			get { return Umbraco.Web.PublishedContentModels.SummaryInfo.GetSummaryText(this); }
		}

		///<summary>
		/// Category
		///</summary>
		[ImplementPropertyType("category")]
		public IEnumerable<string> Category
		{
			get { return Umbraco.Web.PublishedContentModels.Taxonomy.GetCategory(this); }
		}

		///<summary>
		/// Tags
		///</summary>
		[ImplementPropertyType("tags")]
		public IEnumerable<string> Tags
		{
			get { return Umbraco.Web.PublishedContentModels.Taxonomy.GetTags(this); }
		}
	}
}
