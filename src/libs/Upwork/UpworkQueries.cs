namespace Upwork;

internal static class UpworkQueries
{
    public const string PublicMarketplaceJobPostingsSearch =
        """
        query publicMarketplaceJobPostingsSearch($marketPlaceJobFilter: PublicMarketplaceJobPostingsSearchFilter!) {
          publicMarketplaceJobPostingsSearch(marketPlaceJobFilter: $marketPlaceJobFilter) {
            jobs {
              id
              title
              createdDateTime
              type
              ciphertext
              description
              skills {
                name
                prettyName
                highlighted
              }
              engagement
              amount {
                rawValue
                currency
                displayValue
              }
              recno
              contractorTier
              jobStatus
              client {
                totalHires
                totalPostedJobs
                totalReviews
                totalFeedback
                verificationStatus
                location {
                  country
                  city
                  state
                  countryTimezone
                  worldRegion
                }
              }
              category
              subcategory
              freelancersToHire
              enterpriseJob
              jobTs
              totalApplicants
              prefFreelancerLocationMandatory
              publishedDateTime
              local
              locations {
                country
                city
                state
                countryTimezone
                worldRegion
              }
              durationLabel
              applied
              ontologySkills {
                id
                prefLabel
                highlighted
                freeText
              }
              duration
              hourlyBudgetType
              hourlyBudgetMin
              hourlyBudgetMax
              occupations {
                category {
                  id
                  prefLabel
                }
                subCategories {
                  id
                  prefLabel
                }
                occupationService {
                  id
                  prefLabel
                }
              }
              weeklyBudget {
                rawValue
                currency
                displayValue
              }
              engagementDuration {
                id
                label
              }
            }
            paging {
              endCursor
              hasNextPage
            }
            facets {
              jobType
              workload
              clientHires
              budget
              clientFeedback
              daysPosted
              contractorTier
              categories
              payment
              proposals
              duration
              occupations
              freelancersNeeded
            }
          }
        }
        """;

    public const string MarketplaceJobPostingsSearch =
        """
        query marketplaceJobPostingsSearch(
          $marketPlaceJobFilter: MarketplaceJobPostingsSearchFilter,
          $searchType: MarketplaceJobPostingSearchType,
          $sortAttributes: [MarketplaceJobPostingSearchSortAttribute]
        ) {
          marketplaceJobPostingsSearch(
            marketPlaceJobFilter: $marketPlaceJobFilter,
            searchType: $searchType,
            sortAttributes: $sortAttributes
          ) {
            totalCount
            edges {
              cursor
              node {
                id
                title
                description
                ciphertext
                duration
                durationLabel
                engagement
                amount {
                  rawValue
                  currency
                  displayValue
                }
                recordNumber
                experienceLevel
                category
                subcategory
                freelancersToHire
                relevance {
                  id
                  effectiveCandidates
                  recommendedEffectiveCandidates
                  uniqueImpressions
                  publishTime
                  hoursInactive
                }
                enterprise
                relevanceEncoded
                totalApplicants
                preferredFreelancerLocation
                preferredFreelancerLocationMandatory
                premium
                clientNotSureFields
                clientPrivateFields
                applied
                createdDateTime
                publishedDateTime
                renewedDateTime
                client {
                  memberSinceDateTime
                  totalHires
                  totalPostedJobs
                  totalSpent {
                    rawValue
                    currency
                    displayValue
                  }
                  verificationStatus
                  location {
                    city
                    country
                    timezone
                    state
                    offsetToUTC
                  }
                  totalReviews
                  totalFeedback
                  companyRid
                  edcUserId
                  lastContractRid
                  companyOrgUid
                  hasFinancialPrivacy
                }
                skills {
                  id
                  name
                  prettyName
                  highlighted
                }
                occupations {
                  category {
                    id
                    prefLabel
                  }
                  subCategories {
                    id
                    prefLabel
                  }
                  occupationService {
                    id
                    prefLabel
                  }
                }
                hourlyBudgetType
                hourlyBudgetMin {
                  rawValue
                  currency
                  displayValue
                }
                hourlyBudgetMax {
                  rawValue
                  currency
                  displayValue
                }
                localJobUserDistance
                weeklyBudget {
                  rawValue
                  currency
                  displayValue
                }
                engagementDuration {
                  id
                  label
                  weeks
                }
                totalFreelancersToHire
                teamId
                freelancerClientRelation {
                  companyRid
                  companyName
                  edcUserId
                  lastContractPlatform
                  lastContractRid
                  lastContractTitle
                }
              }
            }
            pageInfo {
              endCursor
              hasNextPage
            }
          }
        }
        """;

    public const string MarketplaceJobPosting =
        """
        query marketplaceJobPosting($id: ID!) {
          marketplaceJobPosting(id: $id) {
            id
            workFlowState {
              closeResult
              status
            }
            annotations {
              tags
              customFields {
                key
                value
              }
            }
            content {
              title
              description
            }
            attachments {
              id
              sequenceNumber
              fileName
              fileSize
            }
            additionalSearchInfo {
              highlightTitle
            }
            canClientReceiveContractProposal
          }
        }
        """;

    public const string MarketplaceJobPostingsContents =
        """
        query marketplaceJobPostingsContents($ids: [ID!]!) {
          marketplaceJobPostingsContents(ids: $ids) {
            id
            ciphertext
            title
            description
            publishedDateTime
            annotations {
              tags
              customFields {
                key
                value
              }
            }
          }
        }
        """;

    public const string ProposalMetadata =
        """
        query proposalMetadata($reasonType: ReasonType) {
          proposalMetadata {
            engagementDurationValues {
              id
              label
            }
            reasons(reasonType: $reasonType) {
              id
              reason
              alias
            }
          }
        }
        """;

    public const string VendorProposal =
        """
        query vendorProposal($id: ID!) {
          vendorProposal(id: $id) {
            ...VendorProposalFields
          }
        }

        fragment VendorProposalFields on VendorProposal {
          id
          marketplaceJobPosting {
            id
            content {
              title
              description
            }
            canClientReceiveContractProposal
          }
          terms {
            chargeRate {
              rawValue
              currency
              displayValue
            }
            estimatedDuration {
              id
              label
            }
            upfrontPaymentPercent
          }
          coverLetter
          proposalCoverLetter
          projectPlan {
            id
            milestones {
              description
              dueDate
              amount
            }
          }
          auditDetails {
            createdDateTime
            modifiedDateTime
          }
          status {
            status
            reason {
              id
              reason
              description
            }
          }
          annotations
        }
        """;

    public const string VendorProposals =
        """
        query vendorProposals(
          $filter: VendorProposalFilter!,
          $sortAttribute: VendorProposalSortAttribute!,
          $pagination: Pagination!
        ) {
          vendorProposals(
            filter: $filter,
            sortAttribute: $sortAttribute,
            pagination: $pagination
          ) {
            totalCount
            edges {
              cursor
              node {
                ...VendorProposalFields
              }
            }
            pageInfo {
              endCursor
              hasNextPage
            }
          }
        }

        fragment VendorProposalFields on VendorProposal {
          id
          marketplaceJobPosting {
            id
            content {
              title
              description
            }
            canClientReceiveContractProposal
          }
          terms {
            chargeRate {
              rawValue
              currency
              displayValue
            }
            estimatedDuration {
              id
              label
            }
            upfrontPaymentPercent
          }
          coverLetter
          proposalCoverLetter
          projectPlan {
            id
            milestones {
              description
              dueDate
              amount
            }
          }
          auditDetails {
            createdDateTime
            modifiedDateTime
          }
          status {
            status
            reason {
              id
              reason
              description
            }
          }
          annotations
        }
        """;

    public const string ConfirmFiles =
        """
        mutation confirmFiles($fileIds: [ID!]!, $skipMissing: Boolean) {
          confirmFiles(fileIds: $fileIds, skipMissing: $skipMissing)
        }
        """;

    public const string CreateDirectUploadLinkForJobApplicationProposal =
        """
        mutation createDirectUploadLinkForJAClientProposal($input: CreateDirectUploadLinkInput!) {
          createDirectUploadLinkForJAClientProposal(input: $input) {
            id
            uploadUrl
            formKeyValues {
              key
              value
            }
          }
        }
        """;
}
