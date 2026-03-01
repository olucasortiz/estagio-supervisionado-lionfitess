using LionFitness.Application.DTOS.Member;
using LionFitness.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LionFitness.Api.Controllers;

[ApiController]
[Route("api/members")]
public sealed class MembersController : ControllerBase
{
    private readonly IMemberService _service;

    public MembersController(IMemberService service)
    {
        _service = service;
    }

    // POST /api/members
    [HttpPost]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<MemberResponse>> Create(
        [FromBody] MemberCreateRequest request,
        CancellationToken ct)
    {
        var created = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // GET /api/members/{id}
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberResponse>> GetById(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var member = await _service.GetByIdAsync(id, ct);
        if (member is null)
            return NotFound();

        return Ok(member);
    }

    // GET /api/members
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MemberResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MemberResponse>>> GetAll(CancellationToken ct)
    {
        var members = await _service.GetAllAsync(ct);
        return Ok(members);
    }

    // PUT /api/members/{id}
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] MemberUpdateRequest request,
        CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // PATCH /api/members/{id}/deactivate
    [HttpPatch("{id:int}/deactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(
        [FromRoute] int id,
        CancellationToken ct)
    {
        var ok = await _service.DeactivateAsync(id, ct);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}