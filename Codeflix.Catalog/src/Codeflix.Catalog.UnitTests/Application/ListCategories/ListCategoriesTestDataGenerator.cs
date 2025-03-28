﻿using Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories
{
    public class ListCategoriesTestDataGenerator
    {
        public static IEnumerable<object[]> GetInputsWithoutAllParameter(int times = 14)
        {
            var fixture = new ListCategoriesTestFixture();
            var inputExample = fixture.GetExampleInput();
            for (int i = 0; i < times; i++)
            {
                switch (i % 7)
                {
                    case 0:
                        yield return new object[] {
                        new ListCategoriesRequest()
                    };
                        break;
                    case 1:
                        yield return new object[] {
                        new ListCategoriesRequest(inputExample.Page)
                    };
                        break;
                    case 3:
                        yield return new object[] {
                        new ListCategoriesRequest(
                            inputExample.Page,
                            inputExample.PerPage
                        )
                    };
                        break;
                    case 4:
                        yield return new object[] {
                        new ListCategoriesRequest(
                            inputExample.Page,
                            inputExample.PerPage,
                            inputExample.Search
                        )
                    };
                        break;
                    case 5:
                        yield return new object[] {
                        new ListCategoriesRequest(
                            inputExample.Page,
                            inputExample.PerPage,
                            inputExample.Search,
                            inputExample.Sort
                        )
                    };
                        break;
                    case 6:
                        yield return new object[] { inputExample };
                        break;
                    default:
                        yield return new object[] {
                        new ListCategoriesRequest()
                    };
                        break;
                }
            }
        }
    }
}
