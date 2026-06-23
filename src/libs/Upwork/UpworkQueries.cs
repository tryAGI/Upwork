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
                job {
                  ...MarketplaceJobPostingFields
                }
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
                  companyName
                  edcUserId
                  lastContractPlatform
                  lastContractRid
                  lastContractTitle
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

        fragment MarketplaceJobPostingFields on MarketplaceJobPosting {
          id
          workFlowState {
            closeResult
            status
          }
          ownership {
            company {
              id
              rid
              name
              type
              legacyType
              photoUrl
            }
            team {
              id
              rid
              name
              type
              legacyType
              photoUrl
            }
          }
          annotations {
            tags
            customFields {
              key
              value
            }
          }
          activityStat {
            applicationsBidStats {
              avgRateBid {
                rawValue
                currency
                displayValue
              }
              minRateBid {
                rawValue
                currency
                displayValue
              }
              maxRateBid {
                rawValue
                currency
                displayValue
              }
              avgInterviewedRateBid {
                rawValue
                currency
                displayValue
              }
            }
            jobActivity {
              lastClientActivity
              invitesSent
              totalInvitedToInterview
              totalHired
              totalUnansweredInvites
              totalOffered
              totalRecommended
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
          classification {
            category {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            subCategory {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            occupation {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            skills {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            additionalSkills {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
          }
          segmentationData {
            segmentationValues {
              customValue
              segmentationInfo {
                id
                label
                referenceName
                sortOrder
                segmentationType {
                  id
                  name
                  referenceName
                }
                skill {
                  id
                  ontologyId
                  type
                  entityStatus
                  preferredLabel
                  definition
                  createdDateTime
                  modifiedDateTime
                }
              }
            }
          }
          contractTerms {
            contractStartDate
            contractEndDate
            contractType
            onSiteType
            personsToHire
            experienceLevel
            notSurePersonsToHire
            notSureExperiencelevel
            fixedPriceContractTerms {
              amount {
                rawValue
                currency
                displayValue
              }
              maxAmount {
                rawValue
                currency
                displayValue
              }
              engagementDuration {
                id
                label
                weeks
              }
            }
            hourlyContractTerms {
              engagementDuration {
                id
                label
                weeks
              }
              engagementType
              notSureProjectDuration
              hourlyBudgetType
              hourlyBudgetMin
              hourlyBudgetMax
            }
          }
          contractorSelection {
            proposalRequirement {
              coverLetterRequired
              freelancerMilestonesAllowed
              screeningQuestions {
                question
                sequenceNumber
              }
            }
            qualification {
              contractorType
              englishProficiency
              hasPortfolio
              hoursWorked
              risingTalent
              jobSuccessScore
              minEarning
              preferredGroups {
                id
                name
                logo
              }
              preferenceTests {
                id
                name
              }
            }
            location {
              countries
              states
              timezones
              localCheckRequired
              localMarket
              areas {
                id
                areaType
                name
              }
              notSureLocationPreference
              localDescription
              localFlexibilityDescription
            }
          }
          additionalSearchInfo {
            highlightTitle
          }
          clientCompanyPublic {
            id
            legacyType
            teamsEnabled
            canHire
            hidden
            state
            city
            timezone
            accountingEntity
            billingType
          }
          canClientReceiveContractProposal
        }
        """;

    public const string MarketplaceJobPosting =
        """
        query marketplaceJobPosting($id: ID!) {
          marketplaceJobPosting(id: $id) {
            ...MarketplaceJobPostingFields
          }
        }

        fragment MarketplaceJobPostingFields on MarketplaceJobPosting {
          id
          workFlowState {
            closeResult
            status
          }
          ownership {
            company {
              id
              rid
              name
              type
              legacyType
              photoUrl
            }
            team {
              id
              rid
              name
              type
              legacyType
              photoUrl
            }
          }
          annotations {
            tags
            customFields {
              key
              value
            }
          }
          activityStat {
            applicationsBidStats {
              avgRateBid {
                rawValue
                currency
                displayValue
              }
              minRateBid {
                rawValue
                currency
                displayValue
              }
              maxRateBid {
                rawValue
                currency
                displayValue
              }
              avgInterviewedRateBid {
                rawValue
                currency
                displayValue
              }
            }
            jobActivity {
              lastClientActivity
              invitesSent
              totalInvitedToInterview
              totalHired
              totalUnansweredInvites
              totalOffered
              totalRecommended
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
          classification {
            category {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            subCategory {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            occupation {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            skills {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
            additionalSkills {
              id
              ontologyId
              type
              entityStatus
              preferredLabel
              definition
              createdDateTime
              modifiedDateTime
            }
          }
          segmentationData {
            segmentationValues {
              customValue
              segmentationInfo {
                id
                label
                referenceName
                sortOrder
                segmentationType {
                  id
                  name
                  referenceName
                }
                skill {
                  id
                  ontologyId
                  type
                  entityStatus
                  preferredLabel
                  definition
                  createdDateTime
                  modifiedDateTime
                }
              }
            }
          }
          contractTerms {
            contractStartDate
            contractEndDate
            contractType
            onSiteType
            personsToHire
            experienceLevel
            notSurePersonsToHire
            notSureExperiencelevel
            fixedPriceContractTerms {
              amount {
                rawValue
                currency
                displayValue
              }
              maxAmount {
                rawValue
                currency
                displayValue
              }
              engagementDuration {
                id
                label
                weeks
              }
            }
            hourlyContractTerms {
              engagementDuration {
                id
                label
                weeks
              }
              engagementType
              notSureProjectDuration
              hourlyBudgetType
              hourlyBudgetMin
              hourlyBudgetMax
            }
          }
          contractorSelection {
            proposalRequirement {
              coverLetterRequired
              freelancerMilestonesAllowed
              screeningQuestions {
                question
                sequenceNumber
              }
            }
            qualification {
              contractorType
              englishProficiency
              hasPortfolio
              hoursWorked
              risingTalent
              jobSuccessScore
              minEarning
              preferredGroups {
                id
                name
                logo
              }
              preferenceTests {
                id
                name
              }
            }
            location {
              countries
              states
              timezones
              localCheckRequired
              localMarket
              areas {
                id
                areaType
                name
              }
              notSureLocationPreference
              localDescription
              localFlexibilityDescription
            }
          }
          additionalSearchInfo {
            highlightTitle
          }
          clientCompanyPublic {
            id
            legacyType
            teamsEnabled
            canHire
            hidden
            state
            city
            timezone
            accountingEntity
            billingType
          }
          canClientReceiveContractProposal
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

    public const string CompanySelector =
        """
        query companySelector {
          companySelector {
            items {
              title
              photoUrl
              organizationId
              organizationRid
              organizationType
              organizationLegacyType
              organizationEnterpriseType
              legacyEnterpriseOrganization
              typeTitle
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

}
