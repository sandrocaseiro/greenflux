using Greenflux.Helpers;
using Greenflux.Models;
using Microsoft.AspNetCore.Http;

namespace Greenflux.Exceptions
{
    public enum AppErrors
    {
        SUCCESS,
        BINDING_VALIDATION_ERROR,
        SERVER_ERROR,
        GROUP_NOT_FOUND,
        GROUP_CAPACITY_NOT_AVAILABLE,
        CHARGE_STATION_NOT_FOUND,
        CONNECTOR_ID_ALREADY_EXISTS,
        CONNECTOR_ID_NOT_FOUND
    }

    public static class AppErrorsHelper
    {
        public static int ToHttpStatus(this AppErrors error) =>
            error switch
            {
                AppErrors.SUCCESS => StatusCodes.Status200OK,
                AppErrors.BINDING_VALIDATION_ERROR => StatusCodes.Status400BadRequest,
                AppErrors.GROUP_NOT_FOUND => StatusCodes.Status404NotFound,
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE => StatusCodes.Status400BadRequest,
                AppErrors.CHARGE_STATION_NOT_FOUND => StatusCodes.Status404NotFound,
                AppErrors.CONNECTOR_ID_ALREADY_EXISTS => StatusCodes.Status400BadRequest,
                AppErrors.CONNECTOR_ID_NOT_FOUND => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

        public static VResponseError ToResponseError(this AppErrors error, string message = null) =>
            error switch
            {
                AppErrors.SUCCESS => VResponseError.Success(200, message.DefaultIfNull("Success")),
                AppErrors.BINDING_VALIDATION_ERROR => VResponseError.Error(900, message.DefaultIfNull("Bind validation")),
                AppErrors.GROUP_NOT_FOUND => VResponseError.Error(901, message.DefaultIfNull("Group not found")),
                AppErrors.GROUP_CAPACITY_NOT_AVAILABLE => VResponseError.Error(901, message.DefaultIfNull("Group's available current capacity not enough")),
                AppErrors.CHARGE_STATION_NOT_FOUND => VResponseError.Error(902, message.DefaultIfNull("Charge Station not found")),
                AppErrors.CONNECTOR_ID_ALREADY_EXISTS => VResponseError.Error(903, message.DefaultIfNull("Connector Id already exists for this Charge Station")),
                AppErrors.CONNECTOR_ID_NOT_FOUND => VResponseError.Error(904, message.DefaultIfNull("Connector not found")),
                _ => VResponseError.Error(500, message.DefaultIfNull("Server error")),
            };

        public static void Throw(this AppErrors error) => throw new AppException(error);
    }
}