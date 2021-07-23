Feature: Groups list scenarios

Scenario: List Groups
    When I GET to /v1/groups
    Then I should receive the status code 200
    Then The response data should have 5 items
    Then The response data at index 0 should have a name property containing group1
    Then The response data at index 4 should have a name property containing group5