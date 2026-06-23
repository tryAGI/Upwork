using System.Net;
using System.Net.Http.Headers;

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task GraphQLClient_SendsBearerAndTenantHeaders()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse("""{"data":{"companySelector":{"items":[]}}}"""));

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions
            {
                AccessToken = "Bearer access-token",
                TenantId = "tenant-id",
            });
        await client.GetCompanySelectorAsync();

        handler.Requests.Should().ContainSingle();
        handler.Requests[0].Authorization.Should().Be("Bearer access-token");
        handler.Requests[0].TenantId.Should().Be("tenant-id");
    }

    [TestMethod]
    public async Task GraphQLClient_UsesAccessTokenProvider()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse("""{"data":{"companySelector":{"items":[]}}}"""));

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions
            {
                AccessTokenProvider = new StaticAccessTokenProvider("provided-token"),
            });
        await client.GetCompanySelectorAsync();

        handler.Requests.Should().ContainSingle();
        handler.Requests[0].Authorization.Should().Be("Bearer provided-token");
    }

    [TestMethod]
    public async Task GetCompanySelectorAsync_DeserializesOrganizationContexts()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "companySelector": {
                  "items": [
                    {
                      "title": "Agency",
                      "photoUrl": "https://example.test/logo.png",
                      "organizationId": "org-uid",
                      "organizationRid": "org-rid",
                      "organizationType": "Business",
                      "organizationLegacyType": "Vendor",
                      "organizationEnterpriseType": "Standard",
                      "legacyEnterpriseOrganization": false,
                      "typeTitle": "Agency"
                    }
                  ]
                }
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var selector = await client.GetCompanySelectorAsync();

        var item = selector.Items.Should().ContainSingle().Subject;
        item.Title.Should().Be("Agency");
        item.PhotoUrl.Should().Be(new Uri("https://example.test/logo.png"));
        item.OrganizationId.Should().Be("org-uid");
        item.OrganizationLegacyType.Should().Be("Vendor");
        handler.Requests[0].Content.Should().Contain("companySelector");
    }

    [TestMethod]
    public async Task SearchMarketplaceJobPostingsAsync_UsesCurrentSearchQuery()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "marketplaceJobPostingsSearch": {
                  "totalCount": 1,
                  "edges": [
                    {
                      "cursor": "cursor-1",
                      "node": {
                        "id": "job-1",
                        "job": {
                          "id": "job-1",
                          "ownership": {
                            "company": {
                              "id": "company-1",
                              "rid": "company-rid",
                              "name": "Acme",
                              "type": "BUSINESS",
                              "legacyType": "CLIENT",
                              "photoUrl": "https://example.test/company.png"
                            },
                            "team": {
                              "id": "team-1",
                              "rid": "team-rid",
                              "name": "Platform",
                              "type": "BUSINESS",
                              "legacyType": "CLIENT",
                              "photoUrl": "https://example.test/team.png"
                            }
                          },
                          "activityStat": {
                            "applicationsBidStats": {
                              "avgRateBid": {"rawValue": 75, "currency": "USD", "displayValue": "$75"},
                              "minRateBid": {"rawValue": 50, "currency": "USD", "displayValue": "$50"},
                              "maxRateBid": {"rawValue": 100, "currency": "USD", "displayValue": "$100"},
                              "avgInterviewedRateBid": {"rawValue": 80, "currency": "USD", "displayValue": "$80"}
                            },
                            "jobActivity": {
                              "lastClientActivity": "2026-01-03T00:00:00Z",
                              "invitesSent": 2,
                              "totalInvitedToInterview": 1,
                              "totalHired": 1,
                              "totalUnansweredInvites": 0,
                              "totalOffered": 1,
                              "totalRecommended": 4
                            }
                          },
                          "classification": {
                            "category": {
                              "id": "cat-1",
                              "ontologyId": "upwork:WebMobileSoftwareDev",
                              "type": ["OCCUPATION"],
                              "entityStatus": "ACTIVE",
                              "preferredLabel": "Web, Mobile & Software Dev",
                              "definition": "Software work",
                              "createdDateTime": "2026-01-01T00:00:00Z",
                              "modifiedDateTime": "2026-01-02T00:00:00Z"
                            },
                            "subCategory": null,
                            "occupation": null,
                            "skills": [
                              {
                                "id": "skill-1",
                                "ontologyId": "upwork:CSharp",
                                "type": ["SKILL"],
                                "entityStatus": "ACTIVE",
                                "preferredLabel": "C#",
                                "definition": "C#",
                                "createdDateTime": "2026-01-01T00:00:00Z",
                                "modifiedDateTime": "2026-01-02T00:00:00Z"
                              }
                            ],
                            "additionalSkills": []
                          },
                          "contractTerms": {
                            "contractStartDate": "2026-02-01",
                            "contractEndDate": null,
                            "contractType": "HOURLY",
                            "onSiteType": "REMOTE",
                            "personsToHire": 1,
                            "experienceLevel": "EXPERT",
                            "notSurePersonsToHire": false,
                            "notSureExperiencelevel": false,
                            "fixedPriceContractTerms": null,
                            "hourlyContractTerms": {
                              "engagementDuration": {"id": "duration-1", "label": "1 to 3 months", "weeks": 12},
                              "engagementType": "PART_TIME",
                              "notSureProjectDuration": false,
                              "hourlyBudgetType": "MANUAL",
                              "hourlyBudgetMin": 50,
                              "hourlyBudgetMax": 100
                            }
                          },
                          "clientCompanyPublic": {
                            "id": "company-1",
                            "legacyType": "CLIENT",
                            "teamsEnabled": true,
                            "canHire": true,
                            "hidden": false,
                            "state": "CA",
                            "city": "San Francisco",
                            "timezone": "America/Los_Angeles",
                            "accountingEntity": "US",
                            "billingType": "BILL"
                          },
                          "canClientReceiveContractProposal": true
                        },
                        "title": "Build a GraphQL SDK",
                        "description": "Use C#",
                        "ciphertext": "cipher",
                        "publishedDateTime": "2026-01-02T03:04:05Z",
                        "totalApplicants": 3,
                        "skills": [{"id": "skill-1", "name": "csharp", "prettyName": "C#", "highlighted": true}],
                        "client": {
                          "totalHires": 5,
                          "totalFeedback": 4.9,
                          "verificationStatus": "VERIFIED",
                          "companyName": "Acme",
                          "lastContractPlatform": "Upwork",
                          "lastContractTitle": "Previous SDK"
                        },
                        "amount": {"rawValue": 1000, "currency": "USD", "displayValue": "$1,000"},
                        "hourlyBudgetMin": {"rawValue": 50, "currency": "USD", "displayValue": "$50"},
                        "hourlyBudgetMax": {"rawValue": 100, "currency": "USD", "displayValue": "$100"}
                      }
                    }
                  ],
                  "pageInfo": {"endCursor": "cursor-1", "hasNextPage": false}
                }
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var result = await client.SearchMarketplaceJobPostingsAsync(
            UpworkMarketplaceJobFilters.Keywords("graphql", first: 10));

        result.TotalCount.Should().Be(1);
        result.PageInfo?.HasNextPage.Should().BeFalse();
        var job = result.Edges.Should().ContainSingle().Subject.Node;
        job?.Id.Should().Be("job-1");
        job?.Title.Should().Be("Build a GraphQL SDK");
        job?.TotalApplicants.Should().Be(3);
        job?.Client?.VerificationStatus.Should().Be("VERIFIED");
        job?.Client?.CompanyName.Should().Be("Acme");
        job?.Client?.LastContractTitle.Should().Be("Previous SDK");
        job?.Skills.Should().ContainSingle();
        job?.Job?.Ownership?.Company?.Name.Should().Be("Acme");
        job?.Job?.ActivityStat?.JobActivity?.TotalHired.Should().Be(1);
        job?.Job?.Classification?.Skills.Should().ContainSingle().Which.PreferredLabel.Should().Be("C#");
        job?.Job?.ContractTerms?.HourlyContractTerms?.HourlyBudgetMin.Should().Be(50);
        job?.Job?.ClientCompanyPublic?.CanHire.Should().BeTrue();
        handler.Requests[0].Content.Should().Contain("marketplaceJobPostingsSearch");
        handler.Requests[0].Content.Should().NotContain("marketplaceJobPostings(");
    }

    [TestMethod]
    public async Task GetMarketplaceJobPostingAsync_DeserializesExpandedJobAggregate()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "marketplaceJobPosting": {
                  "id": "job-2",
                  "workFlowState": {"closeResult": null, "status": "OPEN"},
                  "ownership": {
                    "company": {
                      "id": "company-2",
                      "rid": "company-rid-2",
                      "name": "Contoso",
                      "type": "BUSINESS",
                      "legacyType": "CLIENT",
                      "photoUrl": "https://example.test/contoso.png"
                    },
                    "team": null
                  },
                  "annotations": {
                    "tags": ["remote"],
                    "customFields": [{"key": "priority", "value": "high"}]
                  },
                  "activityStat": {
                    "applicationsBidStats": {
                      "avgRateBid": {"rawValue": 90, "currency": "USD", "displayValue": "$90"},
                      "minRateBid": {"rawValue": 70, "currency": "USD", "displayValue": "$70"},
                      "maxRateBid": {"rawValue": 120, "currency": "USD", "displayValue": "$120"},
                      "avgInterviewedRateBid": {"rawValue": 95, "currency": "USD", "displayValue": "$95"}
                    },
                    "jobActivity": {
                      "lastClientActivity": "2026-01-04T00:00:00Z",
                      "invitesSent": 3,
                      "totalInvitedToInterview": 2,
                      "totalHired": 0,
                      "totalUnansweredInvites": 1,
                      "totalOffered": 0,
                      "totalRecommended": 5
                    }
                  },
                  "content": {
                    "title": "Build a marketplace crawler",
                    "description": "Use official GraphQL only"
                  },
                  "attachments": [
                    {"id": "attachment-1", "sequenceNumber": 1, "fileName": "brief.pdf", "fileSize": 1024}
                  ],
                  "classification": {
                    "category": {
                      "id": "cat-2",
                      "ontologyId": "upwork:Development",
                      "type": ["OCCUPATION"],
                      "entityStatus": "ACTIVE",
                      "preferredLabel": "Development",
                      "definition": "Development work",
                      "createdDateTime": "2026-01-01T00:00:00Z",
                      "modifiedDateTime": "2026-01-02T00:00:00Z"
                    },
                    "subCategory": null,
                    "occupation": null,
                    "skills": [
                      {
                        "id": "skill-2",
                        "ontologyId": "upwork:GraphQL",
                        "type": ["SKILL"],
                        "entityStatus": "ACTIVE",
                        "preferredLabel": "GraphQL",
                        "definition": "GraphQL APIs",
                        "createdDateTime": "2026-01-01T00:00:00Z",
                        "modifiedDateTime": "2026-01-02T00:00:00Z"
                      }
                    ],
                    "additionalSkills": []
                  },
                  "segmentationData": {
                    "segmentationValues": [
                      {
                        "customValue": "Backend",
                        "segmentationInfo": {
                          "id": "seg-1",
                          "label": "Project type",
                          "referenceName": "project_type",
                          "sortOrder": 1,
                          "segmentationType": {
                            "id": "seg-type-1",
                            "name": "Project",
                            "referenceName": "project"
                          },
                          "skill": {
                            "id": "skill-3",
                            "ontologyId": "upwork:DotNet",
                            "type": ["SKILL"],
                            "entityStatus": "ACTIVE",
                            "preferredLabel": ".NET",
                            "definition": ".NET",
                            "createdDateTime": "2026-01-01T00:00:00Z",
                            "modifiedDateTime": "2026-01-02T00:00:00Z"
                          }
                        }
                      }
                    ]
                  },
                  "contractTerms": {
                    "contractStartDate": "2026-02-01",
                    "contractEndDate": "2026-04-01",
                    "contractType": "FIXED_PRICE",
                    "onSiteType": "REMOTE",
                    "personsToHire": 1,
                    "experienceLevel": "EXPERT",
                    "notSurePersonsToHire": false,
                    "notSureExperiencelevel": false,
                    "fixedPriceContractTerms": {
                      "amount": {"rawValue": 5000, "currency": "USD", "displayValue": "$5,000"},
                      "maxAmount": {"rawValue": 7500, "currency": "USD", "displayValue": "$7,500"},
                      "engagementDuration": {"id": "duration-2", "label": "3 to 6 months", "weeks": 24}
                    },
                    "hourlyContractTerms": null
                  },
                  "contractorSelection": {
                    "proposalRequirement": {
                      "coverLetterRequired": true,
                      "freelancerMilestonesAllowed": true,
                      "screeningQuestions": [{"question": "Describe your GraphQL experience.", "sequenceNumber": 1}]
                    },
                    "qualification": {
                      "contractorType": "ALL",
                      "englishProficiency": "FLUENT",
                      "hasPortfolio": true,
                      "hoursWorked": 100,
                      "risingTalent": false,
                      "jobSuccessScore": 90,
                      "minEarning": "AT_LEAST_1000",
                      "preferredGroups": [{"id": "group-1", "name": "Top Rated", "logo": "https://example.test/group.png"}],
                      "preferenceTests": [{"id": "test-1", "name": "C#"}]
                    },
                    "location": {
                      "countries": ["US"],
                      "states": ["CA"],
                      "timezones": ["America/Los_Angeles"],
                      "localCheckRequired": false,
                      "localMarket": false,
                      "areas": [{"id": "area-1", "areaType": "CITY", "name": "San Francisco"}],
                      "notSureLocationPreference": false,
                      "localDescription": "Remote",
                      "localFlexibilityDescription": "Flexible"
                    }
                  },
                  "additionalSearchInfo": {"highlightTitle": "<em>GraphQL</em>"},
                  "clientCompanyPublic": {
                    "id": "company-2",
                    "legacyType": "CLIENT",
                    "teamsEnabled": true,
                    "canHire": true,
                    "hidden": false,
                    "state": "CA",
                    "city": "San Francisco",
                    "timezone": "America/Los_Angeles",
                    "accountingEntity": "US",
                    "billingType": "BILL"
                  },
                  "canClientReceiveContractProposal": true
                }
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var job = await client.GetMarketplaceJobPostingAsync("job-2");

        job?.Id.Should().Be("job-2");
        job?.Ownership?.Company?.Name.Should().Be("Contoso");
        job?.Annotations?.CustomFields.Should().ContainSingle().Which.Value?.GetString().Should().Be("high");
        job?.ActivityStat?.ApplicationsBidStats?.AvgInterviewedRateBid?.RawValue.Should().Be(95);
        job?.Classification?.Skills.Should().ContainSingle().Which.PreferredLabel.Should().Be("GraphQL");
        job?.SegmentationData?.SegmentationValues.Should().ContainSingle()
            .Which.SegmentationInfo?.SegmentationType?.ReferenceName.Should().Be("project");
        job?.ContractTerms?.FixedPriceContractTerms?.MaxAmount?.RawValue.Should().Be(7500);
        job?.ContractorSelection?.ProposalRequirement?.ScreeningQuestions.Should().ContainSingle()
            .Which.Question.Should().Contain("GraphQL");
        job?.ContractorSelection?.Qualification?.PreferredGroups.Should().ContainSingle().Which.Name.Should().Be("Top Rated");
        job?.ContractorSelection?.Location?.Areas.Should().ContainSingle().Which.Name.Should().Be("San Francisco");
        job?.ClientCompanyPublic?.City.Should().Be("San Francisco");
        handler.Requests[0].Content.Should().Contain("marketplaceJobPosting");
    }

    [TestMethod]
    public async Task GetMarketplaceJobPostingsContentsAsync_DeserializesContentRecords()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "marketplaceJobPostingsContents": [
                  {
                    "id": "job-1",
                    "ciphertext": "cipher",
                    "title": "Build an SDK",
                    "description": "Detailed description",
                    "publishedDateTime": "2026-01-02T03:04:05Z",
                    "annotations": {"tags": ["remote"]}
                  }
                ]
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var records = await client.GetMarketplaceJobPostingsContentsAsync(["job-1"]);

        var record = records.Should().ContainSingle().Subject;
        record.Id.Should().Be("job-1");
        record.Title.Should().Be("Build an SDK");
        record.Description.Should().Be("Detailed description");
        record.PublishedDateTime.Should().Be(DateTimeOffset.Parse("2026-01-02T03:04:05Z"));
        record.Annotations?.Tags.Should().ContainSingle().Which.Should().Be("remote");
        handler.Requests[0].Content.Should().Contain("marketplaceJobPostingsContents");
    }

    [TestMethod]
    public async Task GraphQLErrors_WithMissingScope_ThrowDedicatedException()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "errors": [
                {
                  "message": "User does not have required OAuth2 permission: Read marketplace Job Postings"
                }
              ],
              "data": null
            }
            """));

        using var client = CreateMockClient(handler);
        var act = async () => await client.SearchMarketplaceJobPostingsAsync();

        await act.Should().ThrowAsync<UpworkMissingScopeException>();
    }

    [TestMethod]
    public async Task HttpErrors_RedactAccessTokenFromExceptionBody()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {"debug":"access-token"}
            """,
            HttpStatusCode.InternalServerError));

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions { AccessToken = "access-token" });
        var act = async () => await client.GetCompanySelectorAsync();

        var exception = await act.Should().ThrowAsync<UpworkHttpException>();
        exception.Which.ResponseBody.Should().NotContain("access-token");
        exception.Which.ResponseBody.Should().Contain("[REDACTED]");
    }

    [TestMethod]
    public async Task RateLimitResponse_ThrowsRetryAfterException()
    {
        var response = JsonResponse("""{"error":"rate limit"}""", HttpStatusCode.TooManyRequests);
        response.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(3));
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(response);

        using var client = CreateMockClient(handler);
        var act = async () => await client.GetCompanySelectorAsync();

        var exception = await act.Should().ThrowAsync<UpworkRateLimitException>();
        exception.Which.RetryAfter.Should().Be(TimeSpan.FromSeconds(3));
    }

    [TestMethod]
    public async Task RateLimitHandler_CanRetryRequest()
    {
        var rateLimit = JsonResponse("""{"error":"rate limit"}""", HttpStatusCode.TooManyRequests);
        rateLimit.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.Zero);
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(rateLimit);
        handler.Enqueue(JsonResponse("""{"data":{"companySelector":{"items":[]}}}"""));
        var rateLimitHandler = new TestRateLimitHandler(TimeSpan.Zero);

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions
            {
                AccessToken = "access-token",
                RateLimitHandler = rateLimitHandler,
                MaxRateLimitRetries = 1,
            });
        var selector = await client.GetCompanySelectorAsync();

        selector.Items.Should().BeEmpty();
        handler.Requests.Should().HaveCount(2);
        rateLimitHandler.Contexts.Should().ContainSingle();
        rateLimitHandler.Contexts[0].RetryAfter.Should().Be(TimeSpan.Zero);
    }
}
