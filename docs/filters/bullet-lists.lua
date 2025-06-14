function BulletList(el)
    local result = {}
    for _, item in ipairs(el.content) do
        for _, block in ipairs(item) do
            table.insert(block.content, 1, pandoc.Str("– "))
            table.insert(result, block)
        end
    end
    return result
end
