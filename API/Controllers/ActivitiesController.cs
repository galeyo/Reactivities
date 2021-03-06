﻿using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ActivitiesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List.ActivitiesEnvelope>> List(
            int? limit,
            int? offset,
            bool isGoing,
            bool isHost,
            DateTime? startDate,
            CancellationToken ct)
        {
            return await Mediator.Send(new List.Query(
                limit, offset, isGoing, isHost, startDate), ct);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ActivityDto>> Details(Guid id, CancellationToken ct)
        {
            return await Mediator.Send(new Details.Query { Id = id }, ct);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command, CancellationToken ct)
        {
            return await Mediator.Send(command, ct);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command, CancellationToken ct)
        {
            command.Id = id;
            return await Mediator.Send(command, ct);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsActivityHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id, CancellationToken ct)
        {
            return await Mediator.Send(new Delete.Command { Id = id }, ct);
        }

        [HttpPost("{id}/attend")]
        public async Task<ActionResult<Unit>> Attend(Guid id)
        {
            return await Mediator.Send(new Attend.Command { Id = id });
        }

        [HttpDelete("{id}/attend")]
        public async Task<ActionResult<Unit>> Unattend(Guid id)
        {
            return await Mediator.Send(new Unattend.Command { Id = id });
        }
    }
}
